namespace LordOfTheRings.Domain;

public sealed class Race
{
    private readonly string _value;

    public Race(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Race cannot be empty.");
        }

        _value = value;
    }

    public override string ToString() => _value;
}