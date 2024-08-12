using LanguageExt;
using LanguageExt.Common;

namespace LordOfTheRings.Domain
{
    public sealed class Character
    {
        private readonly Name _name;
        private readonly Race _race;
        private readonly Weapon _weapon;
        private readonly Region _currentLocation;

        public Character(Name name, Race race, Weapon weapon, Region currentLocation = Region.Shire)
        {
            _name = name;
            _race = race;
            _weapon = weapon;
            _currentLocation = currentLocation;
        }

        public Either<Error, Character> Move(Region destination, Logger logger)
        {
            if (_currentLocation == Region.Mordor && destination != Region.Mordor)
            {
                return Error.New(
                    $"Cannot move {_name} from Mordor to {destination}. Reason: There is no coming back from Mordor.");
            }

            logger(destination != Region.Mordor
                ? $"{_name} moved to {destination}."
                : $"{_name} moved to {destination} 💀.");

            return new Character(_name, _race, _weapon, destination);
        }

        public bool HasName(Name other) => _name == other;
        public bool IsIn(Region region) => _currentLocation == region;
        public string ToStringWithoutRegion() => $"{_name} ({_race}) with {_weapon}";
        public override string ToString() => $"{ToStringWithoutRegion()} in {_currentLocation}";
    }
}