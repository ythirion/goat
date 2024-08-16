namespace LordOfTheRings
{
    public sealed class Character
    {
        public Name Name { get; private set; }
        public Race Race { get; private set; }
        public Weapon Weapon { get; private set; }
        public Region Region { get; private set; } = Region.Shire;

        public Character(Name name, Race race, Weapon weapon)
        {
            Name = name;
            Race = race;
            Weapon = weapon ?? throw new ArgumentException("Character must have a weapon.");
        }

        public void MoveToRegion(Region region)
        {
            if (Region == Region.Mordor && region != Region.Mordor)
            {
                throw new InvalidOperationException(
                    $"Cannot move {Name} from Mordor to {region}. Reason: There is no coming back from Mordor.");
            }

            Region = region;
            Console.WriteLine(region != Region.Mordor ? $"{Name} moved to {region}." : $"{Name} moved to {region} 💀.");
        }

        public void ChangeWeapon(Weapon weapon)
        {
            Weapon = weapon;
        }
    }
}
