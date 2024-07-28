namespace LordOfTheRings.Domain
{
    public sealed class Character
    {
        public Name Name { get; set; }
        public Race Race { get; set; }
        public Weapon Weapon { get; set; }
        public Region CurrentLocation { get; set; } = Region.Shire;

        public override string ToString() => $"{Name} ({Race}) with {Weapon} in {CurrentLocation}";
    }
}