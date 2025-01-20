namespace LordOfTheRings.Domain;

public sealed class Character : IEquatable<Character>
{
    public Identity Identity { get; }
    public Weapon Weapon { get; }
    public Region Region { get; }
    
    public Character(Identity identity, Weapon weapon, Region region)
    {
        Identity = identity;
        Weapon = weapon;
        Region = region;
    }

    public bool InMordor() => Region.IsMordor();

    public bool Equals(Character? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Identity.Equals(other.Identity) && Weapon.Equals(other.Weapon) && Region.Equals(other.Region);
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Character other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Identity, Weapon, Region);
}