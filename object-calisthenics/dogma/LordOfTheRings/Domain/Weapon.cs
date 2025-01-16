namespace LordOfTheRings.Domain;

public sealed class Weapon
{
    public WeaponName Name { get; }
    public Damage Damage { get; }

    public Weapon(WeaponName name, Damage damage)
    {
        Name = name;
        Damage = damage;
    }
}