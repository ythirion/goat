namespace LordOfTheRings
{
    public sealed class Character
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public Weapon Weapon { get; set; }
        public string CurrentLocation { get; set; } = "Shire";
    }
}