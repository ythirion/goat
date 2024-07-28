using FluentAssertions;
using FluentAssertions.LanguageExt;
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
                .RightUnsafe()
                .ToString()
                .Should()
                .Be("Fellowship of the Ring Members:\nFrodo (Hobbit) with Sting in Shire\n");

        [Fact]
        public void Add_Existing_Member_Fails()
            => _fellowship
                .AddMember(_frodo)
                .Bind(f => f.AddMember(_frodo))
                .Should()
                .BeLeft(err => err.Message
                    .Should()
                    .Be("A character with the same name already exists in the fellowship.")
                );

        [Fact]
        public void Remove_Existing_Member()
            => _fellowship
                .AddMember(_frodo)
                .Bind(f => f.Remove(FrodoName.ToName().RightUnsafe()))
                .RightUnsafe()
                .ToString()
                .Should()
                .Be("Fellowship of the Ring Members:\n");

        [Fact]
        public void Move_Members_To_Existing_Region()
            => _fellowship
                .AddMember(_frodo)
                .Bind(f => f.AddMember(_gandalf))
                .Bind(f => f.MoveTo(Region.Moria, _emptyLogger, FrodoName.ToName().RightUnsafe(),
                    GandalfName.ToName().RightUnsafe()))
                .RightUnsafe()
                .ToString()
                .Should()
                .BeEquivalentTo(
                    "Fellowship of the Ring Members:\nFrodo (Hobbit) with Sting in Moria\nGandalf the \ud83d\udc10 (Wizard) with Staff in Moria\n");

        [Fact]
        public void Move_Members_From_Mordor_To_Another_Region_Fails()
            => _fellowship
                .AddMember(_gimli)
                .Bind(f => f.MoveTo(Region.Lothlorien, _emptyLogger, GimliName.ToName().RightUnsafe()))
                .Should()
                .BeLeft(error => error.Message.Should().Be(
                    $"Cannot move Gimli from Mordor to Lothlorien. Reason: There is no coming back from Mordor."
                ));

        [Fact]
        public Task Print_Fellowship_Members_By_Region()
        {
            var log = new StringWriter();
            Action<string> logger = s => log.WriteLine(s);

            _fellowship
                .AddMember(_frodo)
                .Bind(f => f.AddMember(_gandalf))
                .Bind(f => f.AddMember(_gimli))
                .Do(f => f.PrintMembersInRegion(Region.Mordor, logger))
                .Do(f => f.PrintMembersInRegion(Region.Shire, logger))
                .Do(f => f.PrintMembersInRegion(Region.Lothlorien, logger));

            return Verify(log.ToString());
        }
    }
}