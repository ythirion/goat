namespace LordOfTheRings.Domain;

public sealed class Damage
{
    private readonly int _value;

    public Damage(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Damage must be greater than zero.");
        }

        _value = value;
    }

    public override string ToString() => _value.ToString();
}