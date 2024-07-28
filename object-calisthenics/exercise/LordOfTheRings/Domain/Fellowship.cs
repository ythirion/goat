namespace LordOfTheRings.Domain
{
    public sealed class Fellowship()
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
            var characterToRemove = _members.FirstOrDefault(character => character.HasName(name));
            if (characterToRemove == null)
            {
                throw new InvalidOperationException($"No character with the name '{name}' exists in the fellowship.");
            }

            _members.Remove(characterToRemove);

            return this;
        }

        public override string ToString()
            => _members.Aggregate("Fellowship of the Ring Members:\n", (current, member) => current + (member + "\n"));

        public Fellowship MoveTo(Region destination, Logger logger, params Name[] names)
        {
            _members
                .Where(character => ContainsCharacter(names, character))
                .ToList()
                .ForEach(character => character.Move(destination, logger));

            return this;
        }

        private static bool ContainsCharacter(Name[] names, Character character)
            => names.ToList().Exists(character.HasName);

        public void PrintMembersInRegion(Region region, Logger logger)
        {
            var charactersInRegion = _members.Where(m => m.IsIn(region)).ToList();

            if (charactersInRegion.Count == 0)
            {
                logger($"No members in {region}");
                return;
            }

            logger($"Members in {region}:");

            charactersInRegion
                .ToList()
                .ForEach(character => logger(character.ToStringWithoutRegion()));
        }
    }
}