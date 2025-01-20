using LordOfTheRings.Domain;

namespace LordOfTheRings;

public sealed class FellowshipOfTheRing
{
    public List<Character> Members { get; } = [];

    public void Add(Character character)
    {
        if (Members.Contains(character))
        {
            throw new InvalidOperationException("A character with the same name already exists in the fellowship.");
        }
        Members.Add(character);
    }

    public void Remove(Character character) => Members.Remove(character);

    public Character FindByName(CharacterName name)
        => Members.FirstOrDefault(member => member.Identity.Is(name))
           ?? throw new InvalidOperationException($"No character with the name '{name}' exists.");
}