using LordOfTheRings.Domain;

namespace LordOfTheRings;

public sealed class FellowshipOfTheRingService
{
    private readonly List<Character> _members = [];

    public void AddMember(Character character)
    {
        ValidateCharacterUniqueness(character);
        _members.Add(character);
    }

    public void RemoveMember(CharacterName name)
    {
        var character = FindCharacterByName(name);
        _members.Remove(character);
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
        if (_members.Any(existingMember => existingMember.Name.ToString() == character.Name.ToString()))
        {
            throw new InvalidOperationException("A character with the same name already exists in the fellowship.");
        }
    }

    private Character FindCharacterByName(CharacterName name)
        => _members.FirstOrDefault(member => member.Name.ToString() == name.ToString())
           ?? throw new InvalidOperationException($"No character with the name '{name}' exists.");

    private void MoveCharacterToRegion(CharacterName name, Region region)
    {
        var character = FindCharacterByName(name);

        if (character.Region.IsMordor() && !region.IsMordor())
        {
            throw new InvalidOperationException(
                $"Cannot move {character.Name} from Mordor to {region}. Reason: There is no coming back from Mordor.");
        }

        var updatedCharacter = new Character(
            character.Name,
            character.Race,
            character.Weapon,
            region
        );

        _members.Remove(character);
        _members.Add(updatedCharacter);

        Console.WriteLine(!region.IsMordor()
            ? $"{character.Name} moved to {region}."
            : $"{character.Name} moved to {region} 💀.");
    }

    private Character[] FindMembersInRegion(Region region)
        => _members.Where(member => member.Region.ToString() == region.ToString()).ToArray();

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
            Console.WriteLine($"{member.Name} ({member.Race}) with {member.Weapon.Name}");
        }
    }

    private string FormatFellowshipToString()
        => _members.Aggregate(
            "Fellowship of the Ring Members:\n",
            (result, member) =>
                result + $"{member.Name} ({member.Race}) with {member.Weapon.Name} in {member.Region}\n"
        );
}