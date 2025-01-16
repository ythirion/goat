using LordOfTheRings.Domain;

namespace LordOfTheRings.App
{
    public static class App
    {
        public static void Run()
        {
            var fellowship = new FellowshipOfTheRingService();

            try
            {
                AddCharacter(fellowship, "Frodo", "Hobbit", "Sting", 30, "Shire");
                AddCharacter(fellowship, "Sam", "Hobbit", "Dagger", 10, "Shire");
                AddCharacter(fellowship, "Merry", "Hobbit", "Short Sword", 24, "Shire");
                AddCharacter(fellowship, "Pippin", "Hobbit", "Bow", 8, "Shire");
                AddCharacter(fellowship, "Aragorn", "Human", "Anduril", 100, "Shire");
                AddCharacter(fellowship, "Boromir", "Human", "Sword", 90, "Shire");
                AddCharacter(fellowship, "Legolas", "Elf", "Bow", 100, "Shire");
                AddCharacter(fellowship, "Gimli", "Dwarf", "Axe", 100, "Shire");
                AddCharacter(fellowship, "Gandalf the 🐐", "Wizard", "Staff", 200, "Shire");

                Console.WriteLine(fellowship.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            MoveGroupToRegion(fellowship, ["Frodo", "Sam"], "Rivendell");
            MoveGroupToRegion(fellowship, ["Merry", "Pippin", "Aragorn", "Boromir"], "Moria");
            MoveGroupToRegion(fellowship, ["Legolas", "Gimli", "Gandalf the 🐐"], "Lothlorien");

            try
            {
                MoveGroupToRegion(fellowship, ["Frodo", "Sam"], "Mordor");
                MoveGroupToRegion(fellowship, ["Frodo", "Sam"], "Shire"); // This should fail for "Frodo"
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            fellowship.PrintMembersInRegion(new Region("Rivendell"));
            fellowship.PrintMembersInRegion(new Region("Moria"));
            fellowship.PrintMembersInRegion(new Region("Lothlorien"));
            fellowship.PrintMembersInRegion(new Region("Mordor"));
            fellowship.PrintMembersInRegion(new Region("Shire"));

            try
            {
                fellowship.RemoveMember(new CharacterName("Frodo"));
                fellowship.RemoveMember(new CharacterName("Sam")); // This should throw an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddCharacter(FellowshipOfTheRingService fellowship, string name, string race, string weapon,
            int damage, string region)
            => fellowship.AddMember(new Character(
                new CharacterName(name),
                new Race(race),
                new Weapon(new WeaponName(weapon), new Damage(damage)),
                new Region(region)
            ));

        private static void MoveGroupToRegion(FellowshipOfTheRingService fellowship, List<string> names, string region)
        {
            var characterNames = names.Select(name => new CharacterName(name)).ToList();
            fellowship.MoveMembersToRegion(characterNames, new Region(region));
        }
    }
}