# Object Calisthenics Refactoring
## Object Calisthenics
Let's explore the Object Calisthenics that are violated in the current version of the `FellowshipOfTheRingService` and other smells:

```csharp
 public class FellowshipOfTheRingService
{
    // First Class Collections
    private List<Character> members = new List<Character>();

    public void AddMember(Character character)
    {
        // Useless check here
        if (character == null)
        {
            throw new ArgumentNullException(nameof(character), "Character cannot be null.");
        }
        // Don't Abbreviate: many times
        // What is N? Name from the Exception message...
        else if (string.IsNullOrWhiteSpace(character.N))
        {
            throw new ArgumentException("Character must have a name.");
        }
        // Don't use the ELSE keyword: many times in this method
        else if (string.IsNullOrWhiteSpace(character.R))
        {
            throw new ArgumentException("Character must have a race.");
        }
        // Null check everywhere
        else if (character.W == null)
        {
            throw new ArgumentException("Character must have a weapon.");
        }
        // One Dot Per Line: many times
        else if (string.IsNullOrWhiteSpace(character.W.Name))
        {
            throw new ArgumentException("A weapon must have a name.");
        }
        else if (character.W.Damage <= 0)
        {
            throw new ArgumentException("A weapon must have a damage level.");
        }
        else
        {
            // Only One Level Of Indentation Per Method
            bool exists = false;
            foreach (var member in members)
            {
                // No Getters/Setters/Properties
                if (member.N == character.N)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                // Exceptions everywhere...
                throw new InvalidOperationException(
                    "A character with the same name already exists in the fellowship.");
            }
            else
            {
                members.Add(character);
            }
        }
    }

    // Wrap All Primitives And Strings
    public void UpdateCharacterWeapon(string name, string newWeapon, int damage)
    {
        foreach (var character in members)
        {
            if (character.N == name)
            {
                // Anemic objects
                character.W = new Weapon
                {
                    // No Getters/Setters/Properties...
                    Name = newWeapon,
                    Damage = damage
                };
                break;
            }
        }
    }
    ...
}
```

Already a lot of smells in these 2 methods...

## Characterization Test
Before refactoring this code, let's cover the existing software.
We will protect existing behavior of this code against unintended changes through automated testing.

The `Program` is writing a lot of stuff in the `Console` (behavior calls, object states).

```text
Fellowship of the Ring Members:
Frodo (Hobbit) with Sting in Shire
Sam (Hobbit) with Dagger in Shire
Merry (Hobbit) with Short Sword in Shire
Pippin (Hobbit) with Bow in Shire
Aragorn (Human) with Anduril in Shire
Boromir (Human) with Sword in Shire
Legolas (Elf) with Bow in Shire
Gimli (Dwarf) with Axe in Shire
Gandalf the 🐐 (Wizard) with Staff in Shire

Frodo moved to Rivendell.
Sam moved to Rivendell.
Merry moved to Moria.
Pippin moved to Moria.
Aragorn moved to Moria.
Boromir moved to Moria.
Legolas moved to Lothlorien.
Gimli moved to Lothlorien.
Gandalf the 🐐 moved to Lothlorien.
Frodo moved to Mordor 💀.
Sam moved to Mordor 💀.
Cannot move Frodo from Mordor to Shire. Reason: There is no coming back from Mordor.
No members in Rivendell
Members in Moria:
Merry (Hobbit) with Short Sword
Pippin (Hobbit) with Bow
Aragorn (Human) with Anduril
Boromir (Human) with Sword
Members in Lothlorien:
Legolas (Elf) with Bow
Gimli (Dwarf) with Axe
Gandalf the 🐐 (Wizard) with Staff
Members in Mordor:
Frodo (Hobbit) with Sting
Sam (Hobbit) with Dagger
No members in Shire
```

We will cover this behavior by using [Verify](https://github.com/VerifyTests/Verify). 

```csharp
dotnet add package Verify.Xunit --version 26.1.2
```

🔴 Let's add a test on the Program
- We have to introduce a method to encapsulate the behavior in the current `Program` file.

```csharp
public class CharacterizationTest
{
    private readonly StringWriter _console;

    public CharacterizationTest()
    {
        // Use a StringWriter for the Console Output
        _console = new StringWriter();
        // Set the Out for the Console
        Console.SetOut(_console);
    }

    [Fact]
    public Task Program_Should_Run_Same()
    {
        App.Run();
        // Simply call Verify that compares the received and the verified files
        return Verify(_console.ToString());
    }
}
```

At the first run it fails:
![First test run](img/first-run.png)

🟢 We verify the behavior by simply copying the content of the `received` file in the `verified` one

## Surface Refactoring
Let's use our `IDE` to refactor automatically the code to reduce a little bit its complexity.
We activate `Continuous Testing Mode` to assess that we do not break the existing behaviors with those changes.

![Activate Continuous Testing](img/continous-testing.png)

We start by taking good care of the feedback provided:

![Provided feedback](img/ide-feedback.png)

Here is some of the refactorings applied:
- Delete unreachable code
- Remove useless method
- Remove redundant else keywords 
- Introduce Constant
- Convert to `LinQ` expression (`ToString`)

![Remove redundant else keywords](img/remove-redundant-else.png)

And the code now looks like:

```csharp
public class FellowshipOfTheRingService
{
    private const string Mordor = "Mordor";
    private readonly List<Character> _members = [];

    public void AddMember(Character character)
    {
        if (string.IsNullOrWhiteSpace(character.N))
        {
            throw new ArgumentException("Character must have a name.");
        }
        if (string.IsNullOrWhiteSpace(character.R))
        {
            throw new ArgumentException("Character must have a race.");
        }
        if (character.W == null)
        {
            throw new ArgumentException("Character must have a weapon.");
        }
        if (string.IsNullOrWhiteSpace(character.W.Name))
        {
            throw new ArgumentException("A weapon must have a name.");
        }
        if (character.W.Damage <= 0)
        {
            throw new ArgumentException("A weapon must have a damage level.");
        }
        
        var exists = false;
        foreach (var member in _members)
        {
            if (member.N == character.N)
            {
                exists = true;
                break;
            }
        }

        if (exists)
        {
            throw new InvalidOperationException(
                "A character with the same name already exists in the fellowship.");
        }

        _members.Add(character);
    }

    public void RemoveMember(string name)
    {
        Character? characterToRemove = null;
        foreach (var character in _members)
        {
            if (character.N == name)
            {
                characterToRemove = character;
                break;
            }
        }

        if (characterToRemove == null)
        {
            throw new InvalidOperationException($"No character with the name '{name}' exists in the fellowship.");
        }

        _members.Remove(characterToRemove);
    }

    public void MoveMembersToRegion(List<string> memberNames, string region)
    {
        foreach (var name in memberNames)
        {
            foreach (var character in _members)
            {
                if (character.N != name) continue;
                
                if (character.C == Mordor && region != Mordor)
                {
                    throw new InvalidOperationException(
                        $"Cannot move {character.N} from Mordor to {region}. Reason: There is no coming back from Mordor.");
                }

                character.C = region;
                    
                Console.WriteLine(region != Mordor
                    ? $"{character.N} moved to {region}."
                    : $"{character.N} moved to {region} 💀.");
            }
        }
    }

    public void PrintMembersInRegion(string region)
    {
        List<Character> charactersInRegion = new List<Character>();
        foreach (var character in _members)
        {
            if (character.C == region)
            {
                charactersInRegion.Add(character);
            }
        }

        if (charactersInRegion.Count > 0)
        {
            Console.WriteLine($"Members in {region}:");
            foreach (var character in charactersInRegion)
            {
                Console.WriteLine($"{character.N} ({character.R}) with {character.W.Name}");
            }
        }
        else if (charactersInRegion is [])
        {
            Console.WriteLine($"No members in {region}");
        }
    }

    public override string ToString() 
        => _members.Aggregate("Fellowship of the Ring Members:\n", (current, member) => current + $"{member.N} ({member.R}) with {member.W.Name} in {member.C}\n");
}
```