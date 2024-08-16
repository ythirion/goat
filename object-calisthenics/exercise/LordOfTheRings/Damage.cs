namespace LordOfTheRings;

public record Damage
{
    public int Value { get; init; }

    public Damage(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Damage must be greater or equals to 0");
        }
        Value = value;
    }

    public override string ToString() => Value.ToString();
}
