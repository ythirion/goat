namespace LordOfTheRings.Domain;

public sealed class CharacterName
{
    private readonly string _value;

    public CharacterName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Character name cannot be empty.");
        }

        _value = value;
    }

    public override string ToString() => _value;
}