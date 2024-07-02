using Xunit;
using Xunit.Abstractions;

namespace LordOfTheRings.Tests
{
    public class Program
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly StringWriter _output;

        public Program(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _output = new StringWriter();
            Console.SetOut(_output);
        }

        [Fact]
        public void Main()
        {
            var fellowship = new FellowshipOfTheRingService();

            try
            {
                fellowship.AddMember(new Character {N = "Frodo", R = "Hobbit", W = "Sting"});
                fellowship.AddMember(new Character {N = "Sam", R = "Hobbit", W = "Dagger"});
                fellowship.AddMember(new Character {N = "Merry", R = "Hobbit", W = "Short Sword"});
                fellowship.AddMember(new Character {N = "Pippin", R = "Hobbit", W = "Bow"});

                fellowship.AddMember(new Character {N = "Aragorn", R = "Human", W = "Anduril"});
                fellowship.AddMember(new Character {N = "Boromir", R = "Human", W = "Sword"});

                fellowship.AddMember(new Character {N = "Legolas", R = "Elf", W = "Bow"});
                fellowship.AddMember(new Character {N = "Gimli", R = "Dwarf", W = "Axe"});

                fellowship.AddMember(new Character {N = "Gandalf the 🐐", R = "Wizard", W = "Staff"});

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

            _testOutputHelper.WriteLine(_output.ToString());
        }
    }
}