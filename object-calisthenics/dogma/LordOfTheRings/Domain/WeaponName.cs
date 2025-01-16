namespace LordOfTheRings.Domain;

public sealed class WeaponName
{
    private readonly string _value;

    public WeaponName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Weapon name cannot be empty.");
        }

        _value = value;
    }

    public override string ToString() => _value;
}