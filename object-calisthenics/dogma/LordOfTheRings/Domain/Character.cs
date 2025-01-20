namespace LordOfTheRings.Domain;

public sealed class Character : IEquatable<Character>
{
    public CharacterName Name { get; }
    public Race Race { get; }
    public Weapon Weapon { get; }
    public Region Region { get; private set; }

    public Character(CharacterName name, Race race, Weapon weapon, Region region)
    {
        Name = name;
        Race = race;
        Weapon = weapon;
        Region = region;
    }

    public bool Equals(Character? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name.Equals(other.Name) && Race.Equals(other.Race) && Weapon.Equals(other.Weapon) && Region.Equals(other.Region);
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Character other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Name, Race, Weapon, Region);

    public bool InMordor() => Region.IsMordor();
}