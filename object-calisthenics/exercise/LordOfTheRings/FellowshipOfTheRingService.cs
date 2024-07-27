using LordOfTheRings.Domain;

namespace LordOfTheRings
{
    public class FellowshipOfTheRingService
    {
        private readonly List<Character> _members = [];

        public void AddMember(Character character)
        {
            var exists = false;
            foreach (var member in _members)
            {
                if (member.Name == character.Name)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                throw new InvalidOperationException(
                    "A character with the same name already exists in the fellowship.");
            }

            _members.Add(character);
        }

        public void RemoveMember(string name)
        {
            Character? characterToRemove = null;
            foreach (var character in _members)
            {
                if (character.Name == name)
                {
                    characterToRemove = character;
                    break;
                }
            }

            if (characterToRemove == null)
            {
                throw new InvalidOperationException($"No character with the name '{name}' exists in the fellowship.");
            }

            _members.Remove(characterToRemove);
        }

        public void MoveMembersToRegion(List<string> memberNames, string region)
        {
            var regionValue = region.ToRegion();
            foreach (var name in memberNames)
            {
                foreach (var character in _members)
                {
                    if (character.Name != name) continue;

                    if (character.CurrentLocation == Region.Mordor && regionValue != Region.Mordor)
                    {
                        throw new InvalidOperationException(
                            $"Cannot move {character.Name} from Mordor to {region}. Reason: There is no coming back from Mordor.");
                    }

                    character.CurrentLocation = regionValue;

                    Console.WriteLine(regionValue != Region.Mordor
                        ? $"{character.Name} moved to {region}."
                        : $"{character.Name} moved to {region} 💀.");
                }
            }
        }

        public void PrintMembersInRegion(string region)
        {
            List<Character> charactersInRegion = new List<Character>();
            foreach (var character in _members)
            {
                if (character.CurrentLocation == region.ToRegion())
                {
                    charactersInRegion.Add(character);
                }
            }

            if (charactersInRegion.Count > 0)
            {
                Console.WriteLine($"Members in {region}:");
                foreach (var character in charactersInRegion)
                {
                    Console.WriteLine($"{character.Name} ({character.Race}) with {character.Weapon.Name}");
                }
            }
            else if (charactersInRegion is [])
            {
                Console.WriteLine($"No members in {region}");
            }
        }

        public override string ToString()
            => _members.Aggregate("Fellowship of the Ring Members:\n",
                (current, member) =>
                    current + $"{member.Name} ({member.Race}) with {member.Weapon.Name} in {member.CurrentLocation}\n");
    }
}