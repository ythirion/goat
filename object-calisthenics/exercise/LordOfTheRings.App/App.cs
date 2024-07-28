using LordOfTheRings.Domain;

namespace LordOfTheRings.App
{
    public static class App
    {
        private static readonly FellowshipOfTheRingService Fellowship = new();

        public static void Run()
        {
            try
            {
                AddMember("Frodo", Race.Hobbit, "Sting", 30);
                AddMember("Sam", Race.Hobbit, "Dagger", 10);
                AddMember("Merry", Race.Hobbit, "Short Sword", 24);
                AddMember("Pippin", Race.Hobbit, "Bow", 8);
                AddMember("Aragorn", Race.Human, "Anduril", 100);
                AddMember("Boromir", Race.Human, "Sword", 90);
                AddMember("Legolas", Race.Elf, "Bow", 100);
                AddMember("Gimli", Race.Dwarf, "Axe", 100);
                AddMember("Gandalf the 🐐", Race.Wizard, "Staff", 200);

                Console.WriteLine(Fellowship.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var group1 = new List<string> {"Frodo", "Sam"};
            var group2 = new List<string> {"Merry", "Pippin", "Aragorn", "Boromir"};
            var group3 = new List<string> {"Legolas", "Gimli", "Gandalf the 🐐"};

            Fellowship.MoveMembersToRegion(group1, "Rivendell");
            Fellowship.MoveMembersToRegion(group2, "Moria");
            Fellowship.MoveMembersToRegion(group3, "Lothlorien");

            try
            {
                var group4 = new List<string> {"Frodo", "Sam"};
                Fellowship.MoveMembersToRegion(group4, "Mordor");
                Fellowship.MoveMembersToRegion(group4, "Shire"); // This should fail for "Frodo"
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Fellowship.PrintMembersInRegion("Rivendell");
            Fellowship.PrintMembersInRegion("Moria");
            Fellowship.PrintMembersInRegion("Lothlorien");
            Fellowship.PrintMembersInRegion("Mordor");
            Fellowship.PrintMembersInRegion("Shire");

            try
            {
                Fellowship.RemoveMember("Frodo");
                Fellowship.RemoveMember("Sam"); // This should throw an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddMember(
            string name,
            Race race,
            string weapon,
            int damage)
            => Fellowship.AddMember(new Character(name.ToName(), race, new Weapon(weapon.ToName(), damage.ToDamage())));
    }
}