using LordOfTheRings.Domain;

namespace LordOfTheRings;

public sealed class FellowshipOfTheRingService
{
    private readonly FellowshipOfTheRing _fellowship = new();

    public void AddMember(Character character)
    {
        ValidateCharacterUniqueness(character);
        _fellowship.Add(character);
    }

    public void RemoveMember(CharacterName name)
    {
        var character = _fellowship.FindByName(name);
        _fellowship.Remove(character);
    }

    public void MoveMembersToRegion(List<CharacterName> members, Region region)
        => members.ForEach(member => MoveCharacterToRegion(member, region));

    public void PrintMembersInRegion(Region region)
        => PrintMembers(
            FindMembersInRegion(region),
            region
        );

    public override string ToString() => FormatFellowshipToString();

    private void ValidateCharacterUniqueness(Character character)
    {
        if (_fellowship.Contains(character))
        {
            throw new InvalidOperationException("A character with the same name already exists in the fellowship.");
        }
    }

    private void MoveCharacterToRegion(CharacterName name, Region region)
    {
        var character = _fellowship.FindByName(name);

        if (IsTryingToComeBackFromMordor(region, character))
        {
            throw new InvalidOperationException(
                $"Cannot move {name} from Mordor to {region}. Reason: There is no coming back from Mordor.");
        }

        var updatedCharacter = character.MoveTo(region);

        _fellowship.Remove(character);
        _fellowship.Add(updatedCharacter);

        Console.WriteLine(!region.IsMordor()
            ? $"{name} moved to {region}."
            : $"{name} moved to {region} 💀.");
    }

    private static bool IsTryingToComeBackFromMordor(Region region, Character character) =>
        character.InMordor() && !region.IsMordor();

    private Character[] FindMembersInRegion(Region region)
        => _fellowship.MembersInRegion(region);

    private static void PrintMembers(Character[] members, Region region)
    {
        if (members.Length == 0)
        {
            Console.WriteLine($"No members in {region}");
            return;
        }

        Console.WriteLine($"Members in {region}:");
        foreach (var member in members)
        {
            Console.WriteLine($"{member.ToStringWithoutRegion()}");
        }
    }

    private string FormatFellowshipToString() => _fellowship.ToString();
}