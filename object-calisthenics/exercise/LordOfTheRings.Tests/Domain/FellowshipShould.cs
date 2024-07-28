using FluentAssertions;
using LordOfTheRings.Domain;

namespace LordOfTheRings.Tests.Domain
{
    public class FellowshipShould
    {
        private readonly Character _frodo = CharacterBuilder
            .ACharacter("Frodo")
            .Hobbit()
            .Build();

        private readonly Character _gandalf =
            CharacterBuilder
                .ACharacter("Gandalf the 🐐")
                .With("Staff")
                .Wizard()
                .Build();

        private readonly Character _gimli =
            CharacterBuilder
                .ACharacter("Gimli")
                .With("Axe")
                .Dwarf()
                .In(Region.Mordor)
                .Build();

        private readonly Fellowship _fellowship = new();

        [Fact]
        public void Add_New_Member()
            => _fellowship
                .AddMember(_frodo)
                .ToString()
                .Should()
                .Be("Fellowship of the Ring Members:\nFrodo (Hobbit) with Sting in Shire\n");

        [Fact]
        public void Add_Existing_Member_Fails()
        {
            var addFrodoTwice = () => _fellowship
                .AddMember(_frodo)
                .AddMember(_frodo);

            addFrodoTwice.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("A character with the same name already exists in the fellowship.");
        }

        [Fact]
        public void Remove_Existing_Member()
            => _fellowship
                .AddMember(_frodo)
                .Remove(_frodo.Name)
                .ToString()
                .Should()
                .Be("Fellowship of the Ring Members:\n");

        [Fact]
        public void Move_Members_To_Existing_Region()
            => _fellowship
                .AddMember(_frodo)
                .AddMember(_gandalf)
                .MoveTo(Region.Moria, _frodo.Name, _gandalf.Name)
                .ToString()
                .Should()
                .BeEquivalentTo(
                    "Fellowship of the Ring Members:\nFrodo (Hobbit) with Sting in Moria\nGandalf the \ud83d\udc10 (Wizard) with Staff in Moria\n");

        [Fact]
        public void Move_Members_From_Mordor_To_Another_Region_Fails()
        {
            var moveGimliFromMordor = () => _fellowship
                .AddMember(_gimli)
                .MoveTo(Region.Lothlorien, _gimli.Name);

            moveGimliFromMordor
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(
                    $"Cannot move Gimli from Mordor to Lothlorien. Reason: There is no coming back from Mordor.");
        }


        [Fact]
        public Task Print_Fellowship_Members_By_Region()
        {
            var updatedFellowship = _fellowship
                .AddMember(_frodo)
                .AddMember(_gandalf)
                .AddMember(_gimli);

            var originalConsoleOut = Console.Out;
            var log = new StringWriter();
            Console.SetOut(log);

            updatedFellowship.PrintMembersInRegion(Region.Mordor);
            updatedFellowship.PrintMembersInRegion(Region.Shire);
            updatedFellowship.PrintMembersInRegion(Region.Lothlorien);

            Console.SetOut(originalConsoleOut);

            return Verify(log.ToString());
        }
    }
}