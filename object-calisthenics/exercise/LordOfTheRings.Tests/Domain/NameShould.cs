using FluentAssertions;
using FluentAssertions.LanguageExt;
using FsCheck;
using LordOfTheRings.Domain;
using static FsCheck.Arb.Default;

namespace LordOfTheRings.Tests.Domain
{
    public class NameShould
    {
        [Fact]
        public void Parse_A_Valid_Name()
            => Prop.ForAll(
                    NonEmptyString()
                        .Filter(x => !string.IsNullOrWhiteSpace(x.Get)),
                    validName => Name.Parse(validName.Get).RightUnsafe().ToString() == validName.Get)
                .QuickCheckThrowOnFailure();

        [Fact]
        public void Fail_To_Parse_Empty_Name()
            => Name.Parse(string.Empty)
                .Should()
                .BeLeft(err => err.Message.Should().Be("A name can not be empty."));

        [Fact]
        public void Two_Same_Names_Are_Equals()
        {
            var goat1 = Name.Parse("Gandalf the 🐐");
            var goat2 = Name.Parse("Gandalf the 🐐");

            goat1.Should()
                .BeRight(g1 => g1.Should().Be(goat2.RightUnsafe()));
        }
    }
}