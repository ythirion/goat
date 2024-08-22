using FluentAssertions;

namespace LordOfTheRings.Tests;
public sealed class WeaponTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyOrWhitespaceName_ShouldReturnFailureResult(string name)
    {
        // Act
        Result<Weapon> result = Weapon.Create(name, 10);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Weapon name must not be empty.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Create_WithNonPositiveDamage_ShouldReturnFailureResult(int damage)
    {
        // Act
        Result<Weapon> result = Weapon.Create("Sting", damage);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Weapon damage must be greater than zero.");
    }

    [Theory]
    [InlineData("Sting", 10)]
    [InlineData("Anduril", 100)]
    public void Create_WithValidParameters_ShouldReturnSuccessResult(string name, int damage)
    {
        // Act
        Result<Weapon> result = Weapon.Create(name, damage);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(name);
        result.Value.Damage.Should().Be(damage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Update_WithEmptyOrWhitespaceName_ShouldReturnFailureResult(string newName)
    {
        // Arrange
        var initialWeapon = Weapon.Create("Sting", 10).Value;

        // Act
        Result<Weapon> updateResult = initialWeapon.Update(newName, 100);

        // Assert
        updateResult.IsSuccess.Should().BeFalse();
        updateResult.Message.Should().Be("Weapon name must not be empty.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Update_WithNonPositiveDamage_ShouldReturnFailureResult(int newDamage)
    {
        // Arrange
        var initialWeapon = Weapon.Create("Sting", 10).Value;

        // Act
        Result<Weapon> updateResult = initialWeapon.Update("Anduril", newDamage);

        // Assert
        updateResult.IsSuccess.Should().BeFalse();
        updateResult.Message.Should().Be("Weapon damage must be greater than zero.");
    }

    [Theory]
    [InlineData("Anduril", 100)]
    public void Update_WithValidParameters_ShouldReturnSuccessResult(string newName, int newDamage)
    {
        // Arrange
        var initialWeapon = Weapon.Create("Sting", 10).Value;

        // Act
        Result<Weapon> updateResult = initialWeapon.Update(newName, newDamage);

        // Assert
        updateResult.IsSuccess.Should().BeTrue();
        updateResult.Value.Name.Should().Be(newName);
        updateResult.Value.Damage.Should().Be(newDamage);
    }
}