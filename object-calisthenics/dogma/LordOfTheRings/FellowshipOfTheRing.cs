using LordOfTheRings.Domain;

namespace LordOfTheRings;

public sealed class FellowshipOfTheRing
{
    private readonly List<Character> _members = [];
    public List<Character> Members => _members;

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
        => _members.FirstOrDefault(member => member.Name == name)
           ?? throw new InvalidOperationException($"No character with the name '{name}' exists.");
}