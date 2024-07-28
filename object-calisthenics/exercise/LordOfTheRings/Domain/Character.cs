using LanguageExt;
using LanguageExt.Common;

namespace LordOfTheRings.Domain
{
    public sealed class Character(Name name, Race race, Weapon weapon, Region currentLocation = Region.Shire)
    {
        public Either<Error, Character> Move(Region destination, Logger logger)
        {
            if (currentLocation == Region.Mordor && destination != Region.Mordor)
            {
                return Error.New(
                    $"Cannot move {name} from Mordor to {destination}. Reason: There is no coming back from Mordor.");
            }

            currentLocation = destination;
            logger(destination != Region.Mordor
                ? $"{name} moved to {destination}."
                : $"{name} moved to {destination} 💀.");

            return this;
        }

        public bool HasName(Name other) => name == other;
        public bool IsIn(Region region) => currentLocation == region;
        public string ToStringWithoutRegion() => $"{name} ({race}) with {weapon}";
        public override string ToString() => $"{ToStringWithoutRegion()} in {currentLocation}";
    }
}