namespace LordOfTheRings.Domain
{
    public sealed class Fellowship
    {
        private readonly HashSet<Character> _members = [];

        public Fellowship AddMember(Character character)
        {
            if (!_members.Add(character))
            {
                throw new InvalidOperationException(
                    "A character with the same name already exists in the fellowship.");
            }

            return this;
        }

        public Fellowship Remove(Name name)
        {
            var characterToRemove = _members.FirstOrDefault(character => character.Name == name);
            if (characterToRemove == null)
            {
                throw new InvalidOperationException($"No character with the name '{name}' exists in the fellowship.");
            }

            _members.Remove(characterToRemove);

            return this;
        }

        public override string ToString()
            => _members.Aggregate("Fellowship of the Ring Members:\n", (current, member) => current + (member + "\n"));

        public Fellowship MoveTo(Region destination, params Name[] names)
        {
            _members.Where(m => names.Contains(m.Name))
                .ToList()
                .ForEach(character => MoveCharacter(character, destination));

            return this;
        }

        private static void MoveCharacter(Character character, Region destination)
        {
            if (character.CurrentLocation == Region.Mordor && destination != Region.Mordor)
            {
                throw new InvalidOperationException(
                    $"Cannot move {character.Name} from Mordor to {destination}. Reason: There is no coming back from Mordor.");
            }

            character.CurrentLocation = destination;
            Console.WriteLine(destination != Region.Mordor
                ? $"{character.Name} moved to {destination}."
                : $"{character.Name} moved to {destination} 💀.");
        }

        public void PrintMembersInRegion(Region region)
        {
            var charactersInRegion = _members.Where(m => m.CurrentLocation == region).ToList();

            if (charactersInRegion.Count == 0)
            {
                Console.WriteLine($"No members in {region}");
                return;
            }

            Console.WriteLine($"Members in {region}:");
            foreach (var character in charactersInRegion)
            {
                Console.WriteLine($"{character.Name} ({character.Race}) with {character.Weapon.Name}");
            }
        }
    }
}