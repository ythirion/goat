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

## Don't Abbreviate
We adapt the code from the caller `FellowshipOfTheRingService`.
Abbreviations were declared in the `Character` class:

```csharp
public sealed class Character
{
    public string Name { get; set; }
    public string Race { get; set; }
    public Weapon Weapon { get; set; }
    public string CurrentLocation { get; set; } = "Shire";
}
```

## Wrap All Primitives And Strings
There are plenty of Primitives used in the `Domain`...
We can use the [`Sprout` Technique](https://understandlegacycode.com/blog/key-points-of-working-effectively-with-legacy-code/#1-the-sprout-technique) to design new types using [T.D.D](https://tidyfirst.substack.com/p/canon-tdd).
It means that we will design our types on the side of the production code.

The interest of those types is to make impossible to instantiate an object in an invalid state.
To do so, let's apply [`Parse Don't Validate`](https://lexi-lambda.github.io/blog/2019/11/05/parse-don-t-validate/) technique to design our code.

### Name
`Name` is a `string` let's design a new class containing a parsing method.

🔴 Add a first test:

```csharp
[Fact]
public void Parse_A_Valid_Name()
{
    var aValidName = "A valid name because not empty";
    // Name will contain a Parse method with this signature: string -> Name (or Exception 😱)
    // We will go back to exceptions in a few steps
    Name.Parse(aValidName)
        .ToString()
        .Should()
        .Be(aValidName);
}
```

It is red because, the code does not compile:

![Code does not compile](img/failing-tdd-test.png)

🟢 We generate the class from the test and iterate on it to math the test pass

```csharp
public sealed class Name
{
    private readonly string _value;
    private Name(string value) => _value = value;
    // Parse is the only way to instantiate it (Factory Method)
    public static Name Parse(string value) => new(value);
    public override string ToString() => _value;
}
```

🔵 We can refactor our test to use a [`property`](https://xtrem-tdd.netlify.app/Flavours/Testing/pbt) instead of an example

We may express this property like this
```text
for any non empty text
such that parseName is successfull 
```

We use `FsCheck` and results in:

```csharp
// Express how to generate valid names
private static Arbitrary<string> NonEmptyString() => Arb.Default.String()
    .Filter(str => !string.IsNullOrWhiteSpace(str));

[Fact]
public void Parse_A_Valid_Name()
    => Prop.ForAll(
        NonEmptyString(),
        validName => Name.Parse(validName).ToString() == validName)
        .QuickCheckThrowOnFailure();
```

🔴 Let's add a second test

```csharp
[Fact]
public void Fail_To_Parse_Empty_Name()
{
    var act = () => Name.Parse(string.Empty);
    act.Should()
        .Throw<ArgumentException>()
        .WithMessage("A name can not be empty.");
}
```

🟢 Let's adapt the `Name` class

```csharp
public static Name Parse(string value)
{
    if (string.IsNullOrWhiteSpace(value))
    {
        throw new ArgumentException("A name can not be empty.");
    }
    return new Name(value);
}
```

We finish by adding `Equality` on this type and and an extension method on `string`:

```csharp
public sealed class Name
{
    private readonly string _value;
    private Name(string value) => _value = value;
    public static Name Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("A name can not be empty.");
        }
        return new Name(value);
    }

    public override string ToString() => _value;
    
    #region Equality operators

    private bool Equals(Name other) => _value == other._value;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Name other && Equals(other);
    public override int GetHashCode() => _value.GetHashCode();

    #endregion
}

public static class NameExtensions
{
    public static Name ToName(this string value) => Name.Parse(value);
}
```

We are now ready to use it in the production code.
To do so, we change the type in the `Character` class:

```csharp
public sealed class Character
{
    public Name Name { get; set; }
    public string Race { get; set; }
    public Weapon Weapon { get; set; }
    public string CurrentLocation { get; set; } = "Shire";
}
```

Then, the compiler helps us to move on:

![Compiler errors](img/compile-errors.png)

We adapt the production code that looks like this now:
```csharp
public static void Run()
{
    var fellowship = new FellowshipOfTheRingService();

    try
    {
        fellowship.AddMember(new Character
        {
            Name = "Frodo".ToName(), Race = "Hobbit", Weapon = new Weapon
            {
                Name = "Sting",
                Damage = 30
            }
        });

        fellowship.AddMember(new Character
        {
            Name = "Sam".ToName(), Race = "Hobbit", Weapon = new Weapon
            {
                Name = "Dagger",
                Damage = 10
            }
        });
        ...
```

### Race
Let's move on another `string` used in the system: `Race`.
Here, there is a finite number of possible values and yet it is expressed as an infinite one: `string`.

Let's use an `enum` to represent the possible values:

```csharp
public enum Race
{
    Hobbit,
    Human,
    Elf,
    Dwarf,
    Wizard
}
```

### After a few iterations
After having created dedicated types for primitives and string for our `Domain` it looks like this:

![Domain types](img/domain-types.png)

It expresses way much what is going on and we have remove a lot of code from the `Service` (Guard clauses):

```csharp
public void AddMember(Character character)
{
    var exists = false;
    foreach (var member in _members)
    {
        if (member.Name == character.Name)
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
```

The remaining primitive types in the `Service` are coming from the outside world and needs to be parsed.
We may encapsulate those in command objects to make it more cohesive (let's keep it in mind for later on).

```csharp
// Why is a domain object passed here?
public void AddMember(Character character)
public void RemoveMember(string name)
public void MoveMembersToRegion(List<string> memberNames, string region)
public void PrintMembersInRegion(string region)
```