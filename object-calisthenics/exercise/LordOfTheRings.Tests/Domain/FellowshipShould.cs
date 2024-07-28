using FluentAssertions;
using LordOfTheRings.Domain;

namespace LordOfTheRings.Tests.Domain
{
    public class FellowshipShould
    {
        private const string FrodoName = "Frodo";
        private const string GandalfName = "Gandalf the 🐐";
        private const string GimliName = "Gimli";

        private readonly Character _frodo = CharacterBuilder
            .ACharacter(FrodoName)
            .Hobbit()
            .Build();

        private readonly Character _gandalf =
            CharacterBuilder
                .ACharacter(GandalfName)
                .With("Staff")
                .Wizard()
                .Build();

        private readonly Character _gimli =
            CharacterBuilder
                .ACharacter(GimliName)
                .With("Axe")
                .Dwarf()
                .In(Region.Mordor)
                .Build();

        private readonly Fellowship _fellowship = new();
        private readonly Action<string> _emptyLogger = _ => { };

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
                .Remove(FrodoName.ToName())
                .ToString()
                .Should()
                .Be("Fellowship of the Ring Members:\n");

        [Fact]
        public void Move_Members_To_Existing_Region()
            => _fellowship
                .AddMember(_frodo)
                .AddMember(_gandalf)
                .MoveTo(Region.Moria, _emptyLogger, FrodoName.ToName(), GandalfName.ToName())
                .ToString()
                .Should()
                .BeEquivalentTo(
                    "Fellowship of the Ring Members:\nFrodo (Hobbit) with Sting in Moria\nGandalf the \ud83d\udc10 (Wizard) with Staff in Moria\n");

        [Fact]
        public void Move_Members_From_Mordor_To_Another_Region_Fails()
        {
            var moveGimliFromMordor = () => _fellowship
                .AddMember(_gimli)
                .MoveTo(Region.Lothlorien, _emptyLogger, GimliName.ToName());

            moveGimliFromMordor
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(
                    $"Cannot move Gimli from Mordor to Lothlorien. Reason: There is no coming back from Mordor.");
        }

        [Fact]
        public Task Print_Fellowship_Members_By_Region_Without_Console()
        {
            var log = new StringWriter();
            Action<string> logger = s => log.WriteLine(s);

            var updatedFellowship = _fellowship
                .AddMember(_frodo)
                .AddMember(_gandalf)
                .AddMember(_gimli);

            updatedFellowship.PrintMembersInRegion(Region.Mordor, logger);
            updatedFellowship.PrintMembersInRegion(Region.Shire, logger);
            updatedFellowship.PrintMembersInRegion(Region.Lothlorien, logger);

            return Verify(log.ToString());
        }
    }
}