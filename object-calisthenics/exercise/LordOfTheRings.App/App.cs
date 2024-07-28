using LordOfTheRings.Domain;

namespace LordOfTheRings.App
{
    public static class App
    {
        public static void Run(Action<string> logger)
        {
            var fellowship = new FellowshipOfTheRingService(logger);

            AddMember(fellowship, "Frodo", Race.Hobbit, "Sting", 30);
            AddMember(fellowship, "Sam", Race.Hobbit, "Dagger", 10);
            AddMember(fellowship, "Merry", Race.Hobbit, "Short Sword", 24);
            AddMember(fellowship, "Pippin", Race.Hobbit, "Bow", 8);
            AddMember(fellowship, "Aragorn", Race.Human, "Anduril", 100);
            AddMember(fellowship, "Boromir", Race.Human, "Sword", 90);
            AddMember(fellowship, "Legolas", Race.Elf, "Bow", 100);
            AddMember(fellowship, "Gimli", Race.Dwarf, "Axe", 100);
            AddMember(fellowship, "Gandalf the 🐐", Race.Wizard, "Staff", 200);

            logger(fellowship.ToString());

            var group1 = new List<string> {"Frodo", "Sam"};
            var group2 = new List<string> {"Merry", "Pippin", "Aragorn", "Boromir"};
            var group3 = new List<string> {"Legolas", "Gimli", "Gandalf the 🐐"};

            fellowship.MoveMembersToRegion(group1, "Rivendell");
            fellowship.MoveMembersToRegion(group2, "Moria");
            fellowship.MoveMembersToRegion(group3, "Lothlorien");

            var group4 = new List<string> {"Frodo", "Sam"};
            fellowship.MoveMembersToRegion(group4, "Mordor");
            fellowship.MoveMembersToRegion(group4, "Shire");

            fellowship.PrintMembersInRegion("Rivendell");
            fellowship.PrintMembersInRegion("Moria");
            fellowship.PrintMembersInRegion("Lothlorien");
            fellowship.PrintMembersInRegion("Mordor");
            fellowship.PrintMembersInRegion("Shire");

            fellowship.RemoveMember("Frodo");
            fellowship.RemoveMember("Sam"); // This should fail
        }

        private static void AddMember(
            FellowshipOfTheRingService service,
            string name,
            Race race,
            string weapon,
            int damage)
        {
            var character = from n in name.ToName()
                from w in weapon.ToName()
                from d in damage.ToDamage()
                select new Character(n, race, new Weapon(w, d));

            character.IfRight(c => service.AddMember(c));
        }
    }
}