using FluentAssertions;

namespace LordOfTheRings.Tests;
public sealed class CharacterTests
{
    [Theory]
    [InlineData(null, "Hobbit", "Sting", 10, "Character name must not be empty.")]
    [InlineData("Frodo", null, "Sting", 10, "Character race must not be empty.")]
    [InlineData("Frodo", "Hobbit", "", 10, "Weapon name must not be empty.")]
    [InlineData("Frodo", "Hobbit", "Sting", 0, "Weapon damage must be greater than zero.")]
    public void Create_InvalidCharacter_ReturnsFailureResult(
        string name,
        string race,
        string weaponName,
        int damage,
        string expectedMessage)
    {
        // Arrange
        Result<Weapon> weaponResult = null;

        if (weaponName != null)
        {
            weaponResult = Weapon.Create(weaponName, damage);

            if (!weaponResult.IsSuccess)
            {
                weaponResult.Message.Should().Be(expectedMessage);

                return;
            }
        }

        // Act
        Result<Character> characterResult = Character.Create(name, race, weaponResult);

        // Assert
        characterResult.IsSuccess.Should().BeFalse();
        characterResult.Message.Should().Be(expectedMessage);
    }

    [Fact]
    public void Create_ValidCharacter_ReturnsSuccessResult()
    {
        // Arrange
        Result<Weapon> weaponResult = Weapon.Create("Sting", 10);
        Result<Character> characterResult = Character.Create("Frodo", "Hobbit", weaponResult);

        // Assert
        characterResult.IsSuccess.Should().BeTrue();
        var character = characterResult.Value;
        character.Name.Should().Be("Frodo");
        character.Race.Should().Be("Hobbit");
        character.Weapon.Should().Be(weaponResult.Value);
        character.CurrentRegion.Should().Be("Shire");
    }

    [Theory]
    [InlineData("", 10, "Weapon name must not be empty.")]
    [InlineData("Anduril", 0, "Weapon damage must be greater than zero.")]
    public void UpdateWeapon_InvalidWeaponUpdate_ReturnsFailureResult(string newWeaponName, int newDamage, string expectedMessage)
    {
        // Arrange
        Result<Weapon> weaponResult = Weapon.Create("Sting", 10);
        Result<Character> characterResult = Character.Create("Frodo", "Hobbit", weaponResult);

        // Ensure character creation is successful
        characterResult.IsSuccess.Should().BeTrue();
        var character = characterResult.Value;

        // Act
        var updateResult = character.UpdateWeapon(newWeaponName, newDamage);

        // Assert
        updateResult.IsSuccess.Should().BeFalse();
        updateResult.Message.Should().Be(expectedMessage);
    }

    [Theory]
    [InlineData("", 10, "Weapon name must not be empty.")]
    [InlineData("Anduril", 0, "Weapon damage must be greater than zero.")]
    public void Validate_CharacterWithInvalidWeapon_ReturnsFailure(string newWeaponName, int newDamage, string expectedMessage)
    {
        // Arrange
        Result<Weapon> weaponResult = Weapon.Create(newWeaponName, newDamage);

        // Act
        Result<Character> characterResult = Character.Create("Frodo", "Hobbit", weaponResult);

        // Assert
        characterResult.IsSuccess.Should().BeFalse();
        characterResult.Message.Should().Be(expectedMessage);
    }
}