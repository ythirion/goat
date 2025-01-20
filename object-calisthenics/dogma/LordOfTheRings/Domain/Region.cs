namespace LordOfTheRings.Domain;

public sealed class Region : IEquatable<Region>
{
    private readonly string _value;

    public Region(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Region cannot be empty.");
        }

        _value = value;
    }

    public bool IsMordor() => _value == "Mordor";
    public override string ToString() => _value;

    public bool Equals(Region? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Region other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();
}