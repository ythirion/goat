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
                fellowship.AddMember(new Character
                {
                    Name = "Frodo".ToName(), Race = Race.Hobbit, Weapon = new Weapon
                    {
                        Name = "Sting".ToName(),
                        Damage = 30.ToDamage()
                    }
                });

                fellowship.AddMember(new Character
                {
                    Name = "Sam".ToName(), Race = Race.Hobbit, Weapon = new Weapon
                    {
                        Name = "Dagger".ToName(),
                        Damage = 10.ToDamage()
                    }
                });


                fellowship.AddMember(new Character
                {
                    Name = "Merry".ToName(), Race = Race.Hobbit, Weapon = new Weapon
                    {
                        Name = "Short Sword".ToName(),
                        Damage = 24.ToDamage()
                    }
                });

                fellowship.AddMember(new Character
                {
                    Name = "Pippin".ToName(), Race = Race.Hobbit, Weapon = new Weapon
                    {
                        Name = "Bow".ToName(),
                        Damage = 8.ToDamage()
                    }
                });

                fellowship.AddMember(new Character
                {
                    Name = "Aragorn".ToName(), Race = Race.Human, Weapon = new Weapon
                    {
                        Name = "Anduril".ToName(),
                        Damage = 100.ToDamage()
                    }
                });
                fellowship.AddMember(new Character
                {
                    Name = "Boromir".ToName(), Race = Race.Human, Weapon = new Weapon
                    {
                        Name = "Sword".ToName(),
                        Damage = 90.ToDamage()
                    }
                });

                fellowship.AddMember(new Character
                {
                    Name = "Legolas".ToName(), Race = Race.Elf, Weapon = new Weapon
                    {
                        Name = "Bow".ToName(),
                        Damage = 100.ToDamage()
                    }
                });

                fellowship.AddMember(new Character
                {
                    Name = "Gimli".ToName(), Race = Race.Dwarf, Weapon = new Weapon
                    {
                        Name = "Axe".ToName(),
                        Damage = 100.ToDamage()
                    }
                });

                fellowship.AddMember(new Character
                {
                    Name = "Gandalf the 🐐".ToName(), Race = Race.Wizard, Weapon = new Weapon
                    {
                        Name = "Staff".ToName(),
                        Damage = 200.ToDamage()
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
        }
    }
}