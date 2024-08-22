using FluentAssertions;

namespace LordOfTheRings.Tests;
public sealed class FellowshipOfTheRingServiceTests
{
    [Theory]
    [InlineData("Frodo", "Hobbit", "Sting", 10, "Shire")]
    public void AddMember_CharacterWithDuplicateName_ReturnsFailureResult(
        string name,
        string race,
        string weaponName,
        int damage,
        string region)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create(weaponName, damage);
        Result<Character> character1Result = Character.Create(name, race, weaponResult, region);
        Result<Character> character2Result = Character.Create(name, race, weaponResult, region);

        service.AddMember(character1Result.Value);

        // Act
        var result = service.AddMember(character2Result.Value);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("A character with the name 'Frodo' already exists in the fellowship.");
    }

    [Theory]
    [InlineData(null, "Hobbit", "Sting", 10, "Character name must not be empty.")]
    [InlineData("Frodo", null, "Sting", 10, "Character race must not be empty.")]
    [InlineData("Frodo", "Hobbit", null, 10, "Character must have a weapon.")]
    [InlineData("Frodo", "Hobbit", "", 10, "Weapon name must not be empty.")]
    [InlineData("Frodo", "Hobbit", "Sting", 0, "Weapon damage must be greater than zero.")]
    public void AddMember_InvalidCharacter_ReturnsFailureResult(
        string name,
        string race,
        string weaponName,
        int damage,
        string expectedMessage)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        // Ensure that Weapon creation handles null or invalid values correctly
        Result<Weapon> weaponResult = weaponName != null ? Weapon.Create(weaponName, damage) : Result<Weapon>.Failure(expectedMessage);

        // Ensure that Character creation handles null or invalid values correctly
        Result<Character> characterResult = weaponResult.IsSuccess
            ? Character.Create(name, race, weaponResult)
            : Result<Character>.Failure(weaponResult.Message);

        // Act
        var result = characterResult.IsSuccess
            ? service.AddMember(characterResult.Value)
            : characterResult;

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Fact]
    public void AddMember_NullCharacter_ReturnsFailureResult()
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        // Act
        var result = service.AddMember(null);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Character cannot be null.");
    }

    [Theory]
    [InlineData("Frodo", "Hobbit", "Sting", 10, "Shire", "Frodo (Hobbit) with Sting in Shire")]
    [InlineData("Aragorn", "Human", "Sword", 100, "Rivendell", "Aragorn (Human) with Sword in Rivendell")]
    public void AddMember_ValidCharacter_AddsCharacterSuccessfully(
        string name,
        string race,
        string weaponName,
        int damage,
        string region,
        string expectedOutput)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create(weaponName, damage);
        Result<Character> characterResult = Character.Create(name, race, weaponResult, region);

        // Act
        var result = service.AddMember(characterResult.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        service.ToString().Should().Contain(expectedOutput);
    }

    [Theory]
    [InlineData("Frodo", "Mordor", "Shire", "Cannot move Frodo from Mordor to Shire. Reason: There is no coming back from Mordor.")]
    public void MoveMembersToRegion_MovingFromMordor_ReturnsFailureResult(
        string name,
        string fromRegion,
        string toRegion,
        string expectedMessage)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create("Sting", 10);
        Result<Character> characterResult = Character.Create(name, "Hobbit", weaponResult, fromRegion);

        service.AddMember(characterResult.Value);

        var memberNames = new List<string> { name, };

        // Act
        var result = service.MoveMembersToRegion(memberNames, toRegion);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Theory]
    [InlineData("Aragorn", "Human", "Sword", 100, "Rivendell", "Gondor", "Aragorn (Human) with Sword in Gondor")]
    [InlineData("Legolas", "Elf", "Bow", 50, "Rivendell", "Gondor", "Legolas (Elf) with Bow in Gondor")]
    public void MoveMembersToRegion_ValidMembers_MoveSuccessfully(
        string name,
        string race,
        string weaponName,
        int damage,
        string fromRegion,
        string toRegion,
        string expectedOutput)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create(weaponName, damage);
        Result<Character> characterResult = Character.Create(name, race, weaponResult, fromRegion);

        service.AddMember(characterResult.Value);

        var memberNames = new List<string> { name, };

        // Act
        var result = service.MoveMembersToRegion(memberNames, toRegion);

        // Assert
        result.IsSuccess.Should().BeTrue();
        service.ToString().Should().Contain(expectedOutput);
    }

    [Theory]
    [InlineData("Gollum", "Hobbit", "Dagger", 5, "Shire", "Mordor", "Gollum (Hobbit) with Dagger in Mordor")]
    public void MoveMembersToRegion_ValidMoveToMordor_ShowsCorrectOutput(
        string name,
        string race,
        string weaponName,
        int damage,
        string fromRegion,
        string toRegion,
        string expectedOutput)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create(weaponName, damage);
        Result<Character> characterResult = Character.Create(name, race, weaponResult, fromRegion);

        service.AddMember(characterResult.Value);

        var memberNames = new List<string> { name, };

        // Act
        var result = service.MoveMembersToRegion(memberNames, toRegion);

        // Assert
        result.IsSuccess.Should().BeTrue();
        service.ToString().Should().Contain(expectedOutput);
    }

    [Theory]
    [InlineData("Rivendell")]
    [InlineData("Mordor")]
    public void PrintMembersInRegion_RegionHasMembers_PrintsCorrectly(string region)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult1 = Weapon.Create("Sword", 100);
        Result<Character> character1Result = Character.Create("Aragorn", "Human", weaponResult1, "Rivendell");

        Result<Weapon> weaponResult2 = Weapon.Create("Bow", 50);
        Result<Character> character2Result = Character.Create("Legolas", "Elf", weaponResult2, "Rivendell");

        service.AddMember(character1Result.Value);
        service.AddMember(character2Result.Value);

        // Act
        Func<string> act = () => service.GetMembersInRegion(region);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData("Gimli", "No character with the name 'Gimli' exists in the fellowship.")]
    public void RemoveMember_NonExistentMember_ReturnsFailureResult(string name, string expectedMessage)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        // Act
        var result = service.RemoveMember(name);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Theory]
    [InlineData("Boromir", "Human", "Sword", 90, "Gondor")]
    public void RemoveMember_ValidMember_RemovesSuccessfully(
        string name,
        string race,
        string weaponName,
        int damage,
        string region)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create(weaponName, damage);
        Result<Character> characterResult = Character.Create(name, race, weaponResult, region);

        service.AddMember(characterResult.Value);

        // Act
        var result = service.RemoveMember(name);

        // Assert
        result.IsSuccess.Should().BeTrue();
        service.ToString().Should().NotContain(name);
    }

    [Fact]
    public void UpdateCharacterWeapon_CharacterDoesNotExist_ReturnsFailureResult()
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        // Act
        var result = service.UpdateCharacterWeapon("Gimli", "Axe", 80);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("No character with the name 'Gimli' exists in the fellowship.");
    }

    [Theory]
    [InlineData("Legolas", "Longbow", 75)]
    public void UpdateCharacterWeapon_CharacterExists_WeaponUpdatedSuccessfully(string name, string newWeaponName, int newDamage)
    {
        // Arrange
        var service = new FellowshipOfTheRingService();

        Result<Weapon> weaponResult = Weapon.Create("Bow", 50);
        Result<Character> characterResult = Character.Create(name, "Elf", weaponResult, "Rivendell");

        service.AddMember(characterResult.Value);

        // Act
        var result = service.UpdateCharacterWeapon(name, newWeaponName, newDamage);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var expectedOutput = $"{name} (Elf) with {newWeaponName} in Rivendell";
        service.ToString().Should().Contain(expectedOutput);
    }
}