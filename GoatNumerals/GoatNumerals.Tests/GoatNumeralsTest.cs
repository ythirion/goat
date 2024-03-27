using FluentAssertions;
using FluentAssertions.LanguageExt;
using static GoatNumerals.GoatNumeralsConverter;

namespace GoatNumerals.Tests
{
    public class GoatNumeralsTest
    {
        [Fact]
        public void GenerateNoneFor0()
            => Convert(0)
                .Should()
                .BeNone();

        [Theory]
        [InlineData(1, "M")]
        [InlineData(3, "MMM")]
        [InlineData(4, "MBa")]
        [InlineData(5, "Ba")]
        [InlineData(10, "Meh")]
        [InlineData(13, "MehMMM")]
        [InlineData(50, "Baa")]
        [InlineData(100, "Meeh")]
        [InlineData(500, "Baaa")]
        [InlineData(1000, "Meeeh")]
        [InlineData(2499, "MeeehMeeehMeehBaaaMehMeehMMeh")]
        public void GenerateGoatNumeralsForNumbers(int number, string expectedGoatNumeral)
            => Convert(number)
                .Should()
                .BeSome(x => x.Should().Be(expectedGoatNumeral));
    }
}