namespace LordOfTheRings
{
    public class FellowshipOfTheRingService
    {
        private const string Mordor = "Mordor";
        private readonly List<Character> _members = [];

        public void AddMember(Character character)
        {
            if (string.IsNullOrWhiteSpace(character.N))
            {
                throw new ArgumentException("Character must have a name.");
            }
            if (string.IsNullOrWhiteSpace(character.R))
            {
                throw new ArgumentException("Character must have a race.");
            }
            if (character.W == null)
            {
                throw new ArgumentException("Character must have a weapon.");
            }
            if (string.IsNullOrWhiteSpace(character.W.Name))
            {
                throw new ArgumentException("A weapon must have a name.");
            }
            if (character.W.Damage <= 0)
            {
                throw new ArgumentException("A weapon must have a damage level.");
            }
            
            var exists = false;
            foreach (var member in _members)
            {
                if (member.N == character.N)
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
                if (character.N == name)
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
            foreach (var name in memberNames)
            {
                foreach (var character in _members)
                {
                    if (character.N != name) continue;
                    
                    if (character.C == Mordor && region != Mordor)
                    {
                        throw new InvalidOperationException(
                            $"Cannot move {character.N} from Mordor to {region}. Reason: There is no coming back from Mordor.");
                    }

                    character.C = region;
                        
                    Console.WriteLine(region != Mordor
                        ? $"{character.N} moved to {region}."
                        : $"{character.N} moved to {region} 💀.");
                }
            }
        }

        public void PrintMembersInRegion(string region)
        {
            List<Character> charactersInRegion = new List<Character>();
            foreach (var character in _members)
            {
                if (character.C == region)
                {
                    charactersInRegion.Add(character);
                }
            }

            if (charactersInRegion.Count > 0)
            {
                Console.WriteLine($"Members in {region}:");
                foreach (var character in charactersInRegion)
                {
                    Console.WriteLine($"{character.N} ({character.R}) with {character.W.Name}");
                }
            }
            else if (charactersInRegion is [])
            {
                Console.WriteLine($"No members in {region}");
            }
        }

        public override string ToString() 
            => _members.Aggregate("Fellowship of the Ring Members:\n", (current, member) => current + $"{member.N} ({member.R}) with {member.W.Name} in {member.C}\n");
    }
}