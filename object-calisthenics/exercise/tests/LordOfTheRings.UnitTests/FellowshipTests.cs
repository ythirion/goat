using FluentAssertions;

namespace LordOfTheRings.Tests;
public sealed class FellowshipTests
{
    [Fact]
    public void AddMember_ValidCharacter_ShouldAddSuccessfully()
    {
        // Arrange
        var fellowship = new Fellowship();
        Result<Weapon> weaponResult = Weapon.Create("Sting", 10);
        Result<Character> characterResult = Character.Create("Frodo", "Hobbit", weaponResult);

        // Act
        var result = fellowship.AddMember(characterResult.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        fellowship.ToString().Should().Contain("Frodo (Hobbit) with Sting in Shire");
    }
}