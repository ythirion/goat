namespace LordOfTheRings.Domain;

public sealed class FellowshipOfTheRing
{
    private readonly List<Character> _members = [];

    public void Add(Character character)
    {
        if (_members.Contains(character))
        {
            throw new InvalidOperationException("A character with the same name already exists in the fellowship.");
        }

        _members.Add(character);
    }

    public void Remove(Character character) => _members.Remove(character);

    public Character FindByName(CharacterName name)
        => _members.FirstOrDefault(member => member.Is(name))
           ?? throw new InvalidOperationException($"No character with the name '{name}' exists.");

    public Character[] MembersInRegion(Region region)
        => _members
            .Where(member => member.InRegion(region))
            .ToArray();

    public override string ToString()
        => _members.Aggregate(
            "Fellowship of the Ring Members:\n",
            (result, member) => result + $"{member}\n"
        );

    public bool Contains(Character character) => _members.Contains(character);
}