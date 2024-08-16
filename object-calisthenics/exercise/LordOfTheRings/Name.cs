namespace LordOfTheRings;

public record Name
{
    public string Value { get; init; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Character must have a name.");
        }
        Value = value;
    }

    public override string ToString() => Value;
}
