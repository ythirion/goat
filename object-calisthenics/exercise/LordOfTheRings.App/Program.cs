using LordOfTheRings;

var fellowship = new FellowshipOfTheRingService();
try
{
    fellowship.AddMember(new Character(name: new Name("Frodo"), Race.Hobbit, weapon: new Weapon(Name: new Name("Sting"),
        Damage: new Damage(30))));

    fellowship.AddMember(new Character(name: new Name("Sam"), Race.Hobbit, weapon: new Weapon(Name: new Name("Dagger"),
        Damage: new Damage(10))));


    fellowship.AddMember(new Character(name: new Name("Merry"), Race.Hobbit, weapon: new Weapon(Name: new Name("Short Sword"),
        Damage: new Damage(24))));

    fellowship.AddMember(new Character(name: new Name("Pippin"), Race.Hobbit, weapon: new Weapon(Name: new Name("Bow"),
        Damage: new Damage(8))));

    fellowship.AddMember(new Character(name: new Name("Aragorn"), Race.Human, weapon: new Weapon(Name: new Name("Anduril"),
        Damage: new Damage(100))));
    fellowship.AddMember(new Character(name: new Name("Boromir"), Race.Human, weapon: new Weapon(Name: new Name("Sword"),
        Damage: new Damage(90))));

    fellowship.AddMember(new Character(name: new Name("Legolas"), Race.Elf, weapon: new Weapon(Name: new Name("Bow"),
        Damage: new Damage(100))));

    fellowship.AddMember(new Character(name: new Name("Gimli"), Race.Dwarf, weapon: new Weapon(Name: new Name("Axe"),
        Damage: new Damage(100))));

    fellowship.AddMember(new Character(name: new Name("Gandalf the 🐐"), Race.Wizard, weapon: new Weapon(Name: new Name("Staff"),
        Damage: new Damage(200))));

    Console.WriteLine(fellowship.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

var group1 = new List<Name> {new("Frodo"), new("Sam")};
var group2 = new List<Name> {new("Merry"), new("Pippin"), new("Aragorn"), new("Boromir")};
var group3 = new List<Name> {new("Legolas"), new("Gimli"), new("Gandalf the 🐐")};

fellowship.MoveMembersToRegion(group1, Region.Rivendell);
fellowship.MoveMembersToRegion(group2, Region.Moria);
fellowship.MoveMembersToRegion(group3, Region.Lothlorien);

try
{
    var group4 = new List<Name> {new("Frodo"), new("Sam")};
    fellowship.MoveMembersToRegion(group4, Region.Mordor);
    fellowship.MoveMembersToRegion(group4, Region.Shire); // This should fail for "Frodo"
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

fellowship.PrintMembersInRegion(Region.Rivendell);
fellowship.PrintMembersInRegion(Region.Moria);
fellowship.PrintMembersInRegion(Region.Lothlorien);
fellowship.PrintMembersInRegion(Region.Mordor);
fellowship.PrintMembersInRegion(Region.Shire);

try
{
    fellowship.RemoveMember(new("Frodo"));
    fellowship.RemoveMember(new("Sam")); // This should throw an exception
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}   