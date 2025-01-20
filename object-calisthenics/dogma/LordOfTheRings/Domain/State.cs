namespace LordOfTheRings.Domain;

public record State(Weapon Weapon, Region Region)
{
    public bool InMordor() => Region.IsMordor();
}