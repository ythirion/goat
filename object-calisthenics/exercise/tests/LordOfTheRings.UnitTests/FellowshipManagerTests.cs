using FluentAssertions;
using LordOfTheRings.App;

namespace LordOfTheRings.Tests;
public sealed class FellowshipManagerTests
{
    private readonly FellowshipManager _fellowshipManager = new();

    [Fact]
    public void Execute_ShouldProduceExpectedOutput()
    {
        // Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        string expectedOutput =
            "Fellowship of the Ring Members:\n"
            + "Frodo (Hobbit) with Sting in Shire\n"
            + "Sam (Hobbit) with Dagger in Shire\n"
            + "Merry (Hobbit) with Short Sword in Shire\n"
            + "Pippin (Hobbit) with Bow in Shire\n"
            + "Aragorn (Human) with Anduril in Shire\n"
            + "Boromir (Human) with Sword in Shire\n"
            + "Legolas (Elf) with Bow in Shire\n"
            + "Gimli (Dwarf) with Axe in Shire\n"
            + "Gandalf the 🐐 (Wizard) with Staff in Shire\n"
            + "Frodo moved to Rivendell.\n"
            + "Sam moved to Rivendell.\n"
            + "Merry moved to Moria.\n"
            + "Pippin moved to Moria.\n"
            + "Aragorn moved to Moria.\n"
            + "Boromir moved to Moria.\n"
            + "Legolas moved to Lothlorien.\n"
            + "Gimli moved to Lothlorien.\n"
            + "Gandalf the 🐐 moved to Lothlorien.\n"
            + "Frodo moved to Mordor.\n"
            + "Sam moved to Mordor.\n"
            + "Cannot move Frodo from Mordor to Shire. Reason: There is no coming back from Mordor.\n"
            + "No members in Rivendell\n"
            + "Members in Moria:\n"
            + "Merry (Hobbit) with Short Sword\n"
            + "Pippin (Hobbit) with Bow\n"
            + "Aragorn (Human) with Anduril\n"
            + "Boromir (Human) with Sword\n"
            + "Members in Lothlorien:\n"
            + "Legolas (Elf) with Bow\n"
            + "Gimli (Dwarf) with Axe\n"
            + "Gandalf the 🐐 (Wizard) with Staff\n"
            + "Members in Mordor:\n"
            + "Frodo (Hobbit) with Sting\n"
            + "Sam (Hobbit) with Dagger\n"
            + "No members in Shire\n";

        // Act
        _fellowshipManager.Execute();

        // Assert
        var actualOutput = stringWriter.ToString();
        string normalizedExpectedOutput = expectedOutput.Replace("\r\n", "\n");
        string normalizedActualOutput = actualOutput.Replace("\r\n", "\n");

        normalizedActualOutput.Should().Be(normalizedExpectedOutput);
    }
}