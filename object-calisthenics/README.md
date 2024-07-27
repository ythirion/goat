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