namespace LordOfTheRings.Domain;

public sealed class Weapon(WeaponName name, Damage damage)
{
    public WeaponName Name { get; } = name;
    public Damage Damage { get; } = damage;
}