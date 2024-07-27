namespace LordOfTheRings.Domain
{
    public enum Region
    {
        Shire,
        Rivendell,
        Moria,
        Lothlorien,
        Mordor
    }

    public static class RegionExtensions
    {
        public static Region ToRegion(this string region) => Enum.Parse<Region>(region);
    }
}