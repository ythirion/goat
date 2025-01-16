namespace LordOfTheRings.Domain;

public sealed class Region
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
}