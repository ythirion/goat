using FluentAssertions;
using FsCheck;
using LordOfTheRings.Domain;

namespace LordOfTheRings.Tests.Domain
{
    public class NameShould
    {
        [Fact]
        public void Parse_A_Valid_Name()
            => Prop.ForAll(
                    Arb.Default.NonEmptyString(),
                    validName => Name.Parse(validName.Get).ToString() == validName.Get)
                .QuickCheckThrowOnFailure();

        [Fact]
        public void Fail_To_Parse_Empty_Name()
        {
            var act = () => Name.Parse(string.Empty);
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("A name can not be empty.");
        }
    
        [Fact]
        public void Two_Same_Names_Are_Equals()
        {
            var goat1 = Name.Parse("Gandalf the 🐐");
            var goat2 = Name.Parse("Gandalf the 🐐");

            goat1
                .Should()
                .Be(goat2);
        }
    }
}