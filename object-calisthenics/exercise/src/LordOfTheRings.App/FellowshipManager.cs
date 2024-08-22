namespace LordOfTheRings.App;
public sealed class FellowshipManager
{
    private readonly FellowshipOfTheRingService _fellowshipOfTheRingService = new();

    public void Execute()
    {
        CreateAndAddCharacters();
        MoveGroupsToRegions();
        DisplayMembersInRegions();
        RemoveMembers();
    }

    private void CreateAndAddCharacters()
    {
        var characters = new List<Result<Character>>
                         {
                             Character.Create("Frodo", "Hobbit", Weapon.Create("Sting", 30)),
                             Character.Create("Sam", "Hobbit", Weapon.Create("Dagger", 10)),
                             Character.Create("Merry", "Hobbit", Weapon.Create("Short Sword", 24)),
                             Character.Create("Pippin", "Hobbit", Weapon.Create("Bow", 8)),
                             Character.Create("Aragorn", "Human", Weapon.Create("Anduril", 100)),
                             Character.Create("Boromir", "Human", Weapon.Create("Sword", 90)),
                             Character.Create("Legolas", "Elf", Weapon.Create("Bow", 100)),
                             Character.Create("Gimli", "Dwarf", Weapon.Create("Axe", 100)),
                             Character.Create("Gandalf the 🐐", "Wizard", Weapon.Create("Staff", 200)),
                         };

        foreach (Result<Character> characterResult in characters)
        {
            if (characterResult.IsSuccess)
            {
                _fellowshipOfTheRingService.AddMember(characterResult.Value).HandleResult();

                continue;
            }

            Console.WriteLine($"Character creation failed: {characterResult.Message}");
        }

        Console.WriteLine(_fellowshipOfTheRingService.ToString());
    }

    private void DisplayMembersInRegion(string region)
    {
        Console.WriteLine(_fellowshipOfTheRingService.GetMembersInRegion(region));
    }

    private void DisplayMembersInRegions()
    {
        DisplayMembersInRegion("Rivendell");
        DisplayMembersInRegion("Moria");
        DisplayMembersInRegion("Lothlorien");
        DisplayMembersInRegion("Mordor");
        DisplayMembersInRegion("Shire");
    }

    private void MoveGroupsToRegions()
    {
        MoveMembers(["Frodo", "Sam",], "Rivendell");
        MoveMembers(["Merry", "Pippin", "Aragorn", "Boromir",], "Moria");
        MoveMembers(["Legolas", "Gimli", "Gandalf the 🐐",], "Lothlorien");

        var group4 = new List<string> { "Frodo", "Sam", };
        MoveMembers(group4, "Mordor");
        MoveMembers(group4, "Shire");
    }

    private void MoveMembers(List<string> members, string region)
    {
        _fellowshipOfTheRingService.MoveMembersToRegion(members, region).HandleResult(members, region);
    }

    private void RemoveMembers()
    {
        _fellowshipOfTheRingService.RemoveMember("Frodo").HandleResult();
        _fellowshipOfTheRingService.RemoveMember("Sam").HandleResult();
    }
}