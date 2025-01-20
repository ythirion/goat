namespace LordOfTheRings.Domain;

public sealed class State
{
    private readonly Weapon _weapon;
    private readonly Region _region;

    public State(Weapon weapon, Region region)
    {
        _weapon = weapon;
        _region = region;
    }

    public bool InMordor() => _region.IsMordor();

    public State MoveTo(Region region) => new(_weapon, region);

    public bool InRegion(Region region) => _region.Equals(region);

    public string ToStringWeapon() => _weapon.ToString();

    public string ToStringRegion() => _region.ToString();
}