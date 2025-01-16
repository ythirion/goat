namespace LordOfTheRings.Domain
{
    public sealed class Character
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

        public void MoveToRegion(Region newRegion)
        {
            Region = newRegion;
        }
    }
}