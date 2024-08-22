~~# Kata

## First Step Create Unit Test For all services with Copilot

### Step 1

I asked to copilot base on the service to create a unit test for the service.
The goal is to have 100% coverage for the service. Even if coverage is not the goal.

![coverage.jpeg](src%2Fresources%2Fcoverage.jpeg)

### Step 2

refactor: tests to use Theory and InlineData attributes

Refactored test methods in FellowshipOfTheRingServiceTests to use
[Theory] and [InlineData] attributes for parameterized testing.
Updated various test methods to accept parameters for character
details, regions, and expected outcomes, enhancing flexibility
and test coverage. Replaced several [Fact] attributes with
[Theory] and [InlineData].

### Step 3

refactor: Character class and update related services

Refactored the `Character` class to use more descriptive property names:

- Changed `N` to `Name`
- Changed `R` to `Race`
- Changed `W` to `Weapon`
- Changed `C` to `CurrentRegion`

Modified `FellowshipOfTheRingService` to:

- Use the new property names.
- Add validation methods for character properties.
- Ensure unique character names when adding members.
- Simplify the `MoveMembersToRegion` method.
- Add a method to print members in a specific region.
- Add a method to update a character's weapon.
- Refactor the `ToString` method to reflect the new property names.

### Step 4

#### Red Test:

Here What I asked to Gpt4-0

``` 
Instead to throw exception I want to use resultpattern.and move logic inside Character Entitiy.Use TDD.With A red test then a green tests
``` 

#### Green Test:

Added `AddMember_WithDuplicateName_ShouldReturnFailureResult` test to `FellowshipOfTheRingServiceTests` to ensure duplicate members are handled correctly.

- Refactor for Result pattern and add comprehensive tests
- Refactored `Program.cs` to improve error handling and readability using `Result<Character>` objects.
- Updated `FellowshipOfTheRingServiceTest.cs` to align with the new error handling approach, removing old tests and adding new ones.
- Added `CharacterTest.cs` for `Character` class validation.

#### Refactor:

refactor: classes, add tests, and improve error handling
Refactored multiple classes to use the `sealed` keyword for better performance and maintainability.
Implemented the Result pattern for more robust error handling.

### Step 5

#### Red Test:

test: Red - add unit test for Fellowship's AddMember method
Added the `FellowshipTests` class. Implemented the `AddMember_ValidCharacter_ShouldAddSuccessfully` test method, which verifies that a valid character can
be added to a `Fellowship` instance.
The test arranges a new `Fellowship` and `Character` with a `Weapon`, acts by adding the character to the fellowship, and asserts the success of the addition
and the correct string representation of the fellowship.

#### Green Test:

feat: Add Fellowship class and Result pattern; update tests

- Added `FluentAssertions` to `FellowshipTests.cs` for better assertions.
- Introduced `Fellowship` class in `Fellowship.cs` with methods:
    * `AddMember`, `GetCharacterByName`, `GetMembersInRegion`
    * `MoveMembersToRegion`, `Remove`, `ToString`
- Updated `readme.md` to reflect Result pattern and TDD approach.
- Added green test for duplicate member handling.
- Refactored `Program.cs` and `FellowshipOfTheRingServiceTest.cs`.
- Added `CharacterTest.cs` for `Character` class validation.
- Implemented red test for adding a valid character in `FellowshipTests`.

#### Refactor:

refactor: Fellowship classes and update tests
Refactored FellowshipOfTheRingService to delegate member management to the Fellowship class.
Modified Fellowship and FellowshipTests classes to be sealed. Renamed Fellowship.Remove to RemoveMember with added logic.
Updated FellowshipOfTheRingServiceTests to remove hasMembers parameter. Enhanced readme.md with new class and method documentation.
Updated solution settings for code inspection highlighting.

### Step 6

Important: I should added a Golden Test for the program.cs
In order to be sure that the program.cs is working as expected.

feat: update FellowshipOfTheRingService
Added unit tests for FellowshipOfTheRingService in ProgramTest.cs. 


refactor :  solution and introduce FellowshipManager class

- Updated `LordOfTheRings.sln` to remove `SolutionItems` from `src` and add a new `ressources` project.
- Introduced `FellowshipManager` class in `FellowshipManager.cs` to encapsulate character and region management logic.
- Refactored `Program.cs` to use `FellowshipManager`, simplifying main execution.
- Updated `LordOfTheRings.UnitTests.csproj` to include a reference to `LordOfTheRings.App`.
- Refactored `ProgramTest.cs` to use `FellowshipManager` and updated test methods accordingly.~~


