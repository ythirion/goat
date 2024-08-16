using FluentAssertions;

namespace LordOfTheRings.Tests;

public class UnitTest
{
    [Fact]
    public void ShouldFail_WhenDamageIsLessThan0()
    {
        var action = () => { var damage = new Damage(-1); };
        action.Should().Throw<Exception>(); 
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void ShouldFail_WhenNameIs(string? value)
    {
        var action = () => { var name = new Name(value); };
        action.Should().Throw<Exception>(); 
    }
    
    [Fact]
    public void ShouldBeEquals()
    {
        var name1 = new Name("Test1");
        var name2 = new Name("Test1");
        name1.Should().Be(name2);
    }
}