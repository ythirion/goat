namespace LordOfTheRings.Domain;

public record Identity(CharacterName Name, Race Race)
{
    public bool Is(CharacterName name) => Name.Equals(name);
}