# Object Calisthenics Refactoring

## Understanding the project tree

Before launching into any code, I take a look at the projects. One seems to be for testing, and the other an app (so an entry point).
Bad luck, the test project is empty, so it's of no use to me at the moment.
The App project is a console application, and launches with an output that looks like this:

```
Fellowship of the Ring Members:
Frodo (Hobbit) with Sting in Shire
Sam (Hobbit) with Dagger in Shire
Merry (Hobbit) with Short Sword in Shire
Pippin (Hobbit) with Bow in Shire
Aragorn (Human) with Anduril in Shire
Boromir (Human) with Sword in Shire
Legolas (Elf) with Bow in Shire
Gimli (Dwarf) with Axe in Shire
Gandalf the ?? (Wizard) with Staff in Shire

Frodo moved to Rivendell.
Sam moved to Rivendell.
Merry moved to Moria.
Pippin moved to Moria.
Aragorn moved to Moria.
Boromir moved to Moria.
Legolas moved to Lothlorien.
Gimli moved to Lothlorien.
Gandalf the ?? moved to Lothlorien.
Frodo moved to Mordor ??.
Sam moved to Mordor ??.
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
Gandalf the ?? (Wizard) with Staff
Members in Mordor:
Frodo (Hobbit) with Sting
Sam (Hobbit) with Dagger
No members in Shire
```

There seems to be a notion of members, who own a weapon and are located at a place.
Each member can change location.

There's also a new business rule: `Cannot move Frodo from Mordor to Shire. Reason: There is no coming back from Mordor`. It seems that not all moves are allowed, and that some have characteristics.

The end also seems to display a summary of locations once moves have been made.

Looking at the code, it seems that the 1st understanding matches what we deduced from the logs.

There's just one instruction that attracts my curiosity at the end because it doesn't appear in the logs `fellowship.RemoveMember(“Frodo”);`

Now I have two choices:
- Either I'm fairly confident in my understanding of the code, in which case I'll start a refactor straight away
- Or I prefer to be cautious, and add a few tests before starting.

For my part, and to be in an environment close to what I can do in production, I'm going to build a test first.

## Building the golden test

To make sure I don't change the output, I'll copy the code from the `Program` class and build a non-regression test using the Verify lib.

Since the program seems to write to the console, I'll simply retrieve the output from the `Console` and use it with the following lines:

````csharp
var stringWriter = new StringWriter();
Console.SetOut(stringWriter);
````

Now that I've created a safety net, I can tackle the refactor.

## Don't abbreviate

The first thing that shocked me when I read the code was the construction of a `Character` with properties named `N` or `R` :

````csharp
fellowship.AddMember(new Character
{
    N = "Frodo", R = "Hobbit", W = new Weapon
    {
        Name = "Sting",
        Damage = 30
    }
});
````

Pour ça, je vais directement sur la classe `Character` et je vais utiliser le raccourci Rider `Ctrl+R Ctrl+R` sur le nom de la propriété pour la renommée partout.

````csharp
public sealed class Character
{
    public string Name { get; set; }
    public string R { get; set; }
    public Weapon W { get; set; }
    public string C { get; set; } = "Shire";
}
````

I'm going to replace `N` with `Name`, `R` with `Race`, `W` with `Weapon` as these names seem to be the real function behind them, as can be seen in the constructor.

On the other hand, I'm left with a `C` property which I have no idea how it works, it could be `Country` to be linked to location.

Looking into the uses of `C`, I realize that it's more of a region, and rename it `Region`, as can be seen in the code below.

````csharp
if (character.C == "Mordor" && region != "Mordor")
{
    throw new InvalidOperationException(
        $"Cannot move {character.Name} from Mordor to {region}. Reason: There is no coming back from Mordor.");
}
else
{
    character.C = region;
    if (region != "Mordor") Console.WriteLine($"{character.Name} moved to {region}.");
    else Console.WriteLine($"{character.Name} moved to {region} 💀.");
}
````

Le projet `LordOfTheRings` contient 3 classes : `Character`, `Weapon` et `FellowshipOfTheRingService`.
Les 2 premières semblent être des objets anémiques, et la 3e semble contenir toute la logique métier.

La 1ere étape consistait à rendre les propriétés des 2 objets métiers plus pertinentes. Chose faite, le test passe toujours, passons à la suite.

## Keep All Entities Small

To decide what my next step should be, I decide to understand the service provided by `FellowshipOfTheRingService`.

````csharp
public class FellowshipOfTheRingService
{
    private List<Character> members = new List<Character>();

    public void AddMember(Character character)
    public void UpdateCharacterWeapon(string name, string newWeapon, int damage)
    public void RemoveMember(string name)
    public void MoveMembersToRegion(List<string> memberNames, string region)
    public void PrintMembersInRegion(string region)
    public override string ToString()
}
````

It seems to manage 3 different actions: member management, weapon management and region management.

I also remember the list of calisthenics objects, and in particular the first rule `Wrap All Primitives and Strings`.
Looking at the property references for `Weapon` and `Character` (around 20-25 each), I think it's irrelevant to change them directly in these objects, but rather to see how they're used.

The purpose of this rule is also to centralize the rules for creating these objects. So I turn to the `AddMember` method. On closer inspection, I see that it contains rules for creating members, including :

````csharp
else if (string.IsNullOrWhiteSpace(character.Name))
{
    throw new ArgumentException("Character must have a name.");
}
````

So I decided to add these rules to my object's constructor.
This gives me :

````csharp
public sealed class Character
{
    public string Name { get; set; }
    public string Race { get; set; }
    public Weapon Weapon { get; set; }
    public string Region { get; set; } = "Shire";

    public Character(string name, string race, Weapon weapon)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Character must have a name.");
        }
        else if (string.IsNullOrWhiteSpace(race))
        {
            throw new ArgumentException("Character must have a race.");
        }
        else if (weapon == null)
        {
            throw new ArgumentException("Character must have a weapon.");
        }
        else if (string.IsNullOrWhiteSpace(weapon.Name))
        {
            throw new ArgumentException("A weapon must have a name.");
        }
        else if (weapon.Damage <= 0)
        {
            throw new ArgumentException("A weapon must have a damage level.");
        }

        Name = name;
        Race = race;
        Weapon = weapon;
    }   
}
````

````csharp
public void AddMember(Character character)
{
    if (character == null)
    {
        throw new ArgumentNullException(nameof(character), "Character cannot be null.");
    }
    // REMOVED CODE HERE
    else
    {
        bool exists = false;
        foreach (var member in members)
            ....
````

The change of constructor caused me errors in the `Program` and in my test. For this, I can use the refactor to transform the construction of my objects via the new constructor. I use `Alt+Enter` on the code in error, then `Convert initializer to constructor` to obtain :

````csharp
fellowship.AddMember(new Character(name: "Frodo", race: "Hobbit", weapon: new Weapon
{
    Name = "Sting",
    Damage = 30
}));
````

We can do the same thing to change the business rule from `Character` to `Weapon`, so that each has its own constructor.

There's one more step, when we look at the constructor of Character :

````csharp
public Character(string name, string race, Weapon weapon)
````

To do this, we need to replace the notion of `string` for the `name` and `race` parameters.
For `Race`, it seems simplest to construct an `Enum` type, since the values seem limited.
By doing so, we remove the construction rule that was :

````csharp
if (string.IsNullOrWhiteSpace(race))
{
    throw new ArgumentException("Character must have a race.");
}
````

For the `Name` class, I simply pass the business rule into the constructor by changing the exception message:

````csharp
public class Name
{
    public string Value { get; set; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name cannot be null");
        }

        Value = value;
    }
}
````

And for the refactor, I use 2 techniques:
- For Race, I do `Ctrl+Shift+H` to replace everywhere `race:“` with `Race.` and `”, wea` with `, wea`.
- For Name, I use Rider's solution `Replace with new Name(“NAMEHERE”)`.

I still have one last error to correct on the use of `Name` in the `if (character.Name == name)` code, which will simply take `.Value` to get the code to work for now.

Tests turn red with `LordOfTheRings.Name (Hobbit) with Sting in Shire`. We therefore need to change the ToString of the `Name` class.

We're starting to make our domain months anemic, which is a good sign. The tests are still running, which is great!

I can then add a few tests to validate the business rules of my objects:

````csharp
public class UnitTest
{
    [Fact]
    public void ShouldFail_WhenDamageIsLessThan0()
    {
        var action = () => { var damage = new Damage(-1); };
        action.Should().Throw<Exception>(); 
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void ShouldFail_WhenNameIs(string? value)
    {
        var action = () => { var name = new Name(value); };
        action.Should().Throw<Exception>(); 
    }
}
````

Before tackling the creation of the `Region` object, I'm first going to replace all the `string` property calls by the new objects we've created, by replacing the `FellowshipOfTheRingService` function parameters and, in particular, deleting the `.Value` we temporarily added earlier.

When you run the test, you can see that it's no longer working, and the result is really strange. From experience, I suspect that the new comparisons between objects are no longer good. That's why we need to improve comparisons between classes, so that they're no longer based on references, but on values.
The following test verifies the case:

````csharp
[Fact]
public void ShouldBeEquals()
{
    var name1 = new Name("Test1");
    var name2 = new Name("Test1");
    name1.Should().Be(name2); // Red test
}
````

There are 2 ways to do this: either override the `==` operator for the objects, or pass them to record. As these objects are very similar to value objects in DDD, I prefer to set them to record to prevent them from being modified.

Next, I transform everything that talks about `Region` into an enum (like `Race`).

## Don't Use the ELSE Keyword

Now that I understand the purpose of the code, I can do a brief refactor to simplify the writing before moving on.
To do this, I'll start by deleting unnecessary `else` with the `Remove redundant else` option.
At a glance, I've already taken out a few lines.

````csharp
if (exists)
{
    throw new InvalidOperationException(
        "A character with the same name already exists in the fellowship.");
}
else
{
    members.Add(character);
}
````
becomes
````csharp
if (exists)
    throw new InvalidOperationException(
        "A character with the same name already exists in the fellowship.");
members.Add(character);
````

## First Class Collections

Next, I'll try to remove the `private List<Character> members` property in a `Fellowship` class to enable the `AddMember` and `RemoveMember` methods to be integrated. In this way, the management of my list of members is embedded in a single class.

````csharp
public class Fellowship()
{
    public List<Character> Members { get; } = new();
    
    public void AddMember(Character character)
    public void RemoveMember(Name name)
}
````

and in my service 

````csharp
public void AddMember(Character character)
{
    fellowship.AddMember(character);
}

public void RemoveMember(Name name)
{
    fellowship.RemoveMember(name);
}
````

Next, I'm going to continue extracting code from the service, in particular the move management in `MoveMembersToRegion` by replacing the contents of the `if` with

````csharp
if (character.Name == name)
{
    character.MoveToRegion(region);
}
````
which allows me to move the logic directly into the `Character` class.

I do the same for the `Weapon` update, which allows me to set all `Character` properties to `private set`, which is a good sign about the use of my class.

## Refactor

Next, I've simplified the loops, in particular with conditions such as for updating the weapon.
````csharp
public void UpdateCharacterWeapon(Name name, Name newWeapon, Damage damage)
{
    foreach (var character in fellowship.Members.Where(character => character.Name == name))
    {
        character.ChangeWeapon(new Weapon(newWeapon, damage));
        break;
    }
}
````

I'm also cutting out methods to make them simpler, which I'm also moving into `Fellowship`, since it's all about moving members.
````csharp
public void MoveMembersToRegion(List<Name> memberNames, Region region)
{
    foreach (var name in memberNames)
    {
        MoveMemberToRegion(name, region);
    }
}

private void MoveMemberToRegion(Name name, Region region)
{
    foreach (var character in fellowship.Members.Where(character => character.Name == name))
    {
        character.MoveToRegion(region);
    }
}
````
Ditto for retrieving the list of members for display 
````csharp
public List<Character> GetMembersInRegion(Region region)
{
    return Members.Where(character => character.Region == region).ToList();
}
````

And I use proposals to remove the `foreach` as much as possible.

## No Getters/Setters/Properties

In this particular case, I tend to always keep the exposure of my properties according to my needs, so I don't apply this rule to the letter.

The only important point is the setters. I'm careful never to expose them, because if I do, I break the Tell, don't Ask rule, which is to have as much logic as possible inside.
I don't respect it entirely, but limiting setters is a good way of doing it.

## One Dot per Line

As a result of the way we've been divided up, we've automatically respected this rule without really paying attention to it. That's why I take care of it last.

## To go further :

I didn't comply with the following rules:
No Classes with More Than Two Instance Variables
Only One Level of Indentation per Method

This is purely a personal preference, but I don't try to apply these rules at all costs. I find them restrictive in relation to the benefits they bring.
They shouldn't be applied at all costs, but as part of a broader logic.

In the case of the rule: Only One Level of Indentation per Method, a good way is to switch to more functional programming, where one task is broken down by method. It's a rule I'm leaning towards, but I allow myself some deviations.