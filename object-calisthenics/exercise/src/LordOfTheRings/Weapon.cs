namespace LordOfTheRings;
public sealed class Weapon
{
    public int Damage { get; }
    public string Name { get; }

    private Weapon(int damage, string name)
    {
        Damage = damage;
        Name = name;
    }

    public static Result<Weapon> Create(string name, int damage)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Weapon>.Failure("Weapon name must not be empty.");
        }

        if (damage <= 0)
        {
            return Result<Weapon>.Failure("Weapon damage must be greater than zero.");
        }

        return Result<Weapon>.Success(new Weapon(damage, name));
    }

    public override string ToString() => $"{Name} with {Damage} damage";

    public Result<Weapon> Update(string newName, int newDamage) => Create(newName, newDamage);
}