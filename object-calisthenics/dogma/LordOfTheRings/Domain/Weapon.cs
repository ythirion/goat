namespace LordOfTheRings.Domain;

public sealed class Weapon
{
    private readonly WeaponName _name;
    private readonly Damage _damage;

    public Weapon(WeaponName name, Damage damage)
    {
        _name = name;
        _damage = damage;
    }

    public override string ToString() => _name.ToString();
}