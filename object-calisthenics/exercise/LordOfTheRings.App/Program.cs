using LordOfTheRings;

var fellowship = new FellowshipOfTheRingService();

try
{
    fellowship.AddMember(new Character
    {
        N = "Frodo", R = "Hobbit", W = new Weapon
        {
            Name = "Sting",
            Damage = 30
        }
    });

    fellowship.AddMember(new Character
    {
        N = "Sam", R = "Hobbit", W = new Weapon
        {
            Name = "Dagger",
            Damage = 10
        }
    });


    fellowship.AddMember(new Character
    {
        N = "Merry", R = "Hobbit", W = new Weapon
        {
            Name = "Short Sword",
            Damage = 24
        }
    });

    fellowship.AddMember(new Character
    {
        N = "Pippin", R = "Hobbit", W = new Weapon
        {
            Name = "Bow",
            Damage = 8
        }
    });

    fellowship.AddMember(new Character
    {
        N = "Aragorn", R = "Human", W = new Weapon
        {
            Name = "Anduril",
            Damage = 100
        }
    });
    fellowship.AddMember(new Character
    {
        N = "Boromir", R = "Human", W = new Weapon
        {
            Name = "Sword",
            Damage = 90
        }
    });

    fellowship.AddMember(new Character
    {
        N = "Legolas", R = "Elf", W = new Weapon
        {
            Name = "Bow",
            Damage = 100
        }
    });

    fellowship.AddMember(new Character
    {
        N = "Gimli", R = "Dwarf", W = new Weapon
        {
            Name = "Axe",
            Damage = 100
        }
    });

    fellowship.AddMember(new Character
    {
        N = "Gandalf the 🐐", R = "Wizard", W = new Weapon
        {
            Name = "Staff",
            Damage = 200
        }
    });

    Console.WriteLine(fellowship.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

var group1 = new List<string> {"Frodo", "Sam"};
var group2 = new List<string> {"Merry", "Pippin", "Aragorn", "Boromir"};
var group3 = new List<string> {"Legolas", "Gimli", "Gandalf the 🐐"};

fellowship.MoveMembersToRegion(group1, "Rivendell");
fellowship.MoveMembersToRegion(group2, "Moria");
fellowship.MoveMembersToRegion(group3, "Lothlorien");

try
{
    var group4 = new List<string> {"Frodo", "Sam"};
    fellowship.MoveMembersToRegion(group4, "Mordor");
    fellowship.MoveMembersToRegion(group4, "Shire"); // This should fail for "Frodo"
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

fellowship.PrintMembersInRegion("Rivendell");
fellowship.PrintMembersInRegion("Moria");
fellowship.PrintMembersInRegion("Lothlorien");
fellowship.PrintMembersInRegion("Mordor");
fellowship.PrintMembersInRegion("Shire");

try
{
    fellowship.RemoveMember("Frodo");
    fellowship.RemoveMember("Sam"); // This should throw an exception
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}