namespace LordOfTheRings.Domain;

public sealed class CharacterName : IEquatable<CharacterName>
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

    public bool Equals(CharacterName? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is CharacterName other && Equals(other);
    public override int GetHashCode() => _value.GetHashCode();
}