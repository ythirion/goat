using LanguageExt;
using LanguageExt.Common;
using Characters = System.Collections.Generic.HashSet<LordOfTheRings.Domain.Character>;

namespace LordOfTheRings.Domain
{
    public sealed class Fellowship()
    {
        private readonly Characters _members = [];

        public Either<Error, Fellowship> AddMember(Character character)
            => !_members.Add(character)
                ? Error.New("A character with the same name already exists in the fellowship.")
                : this;

        public Either<Error, Fellowship> Remove(Name name)
        {
            var characterToRemove = _members.FirstOrDefault(character => character.HasName(name));

            if (characterToRemove == null)
                return Error.New($"No character with the name '{name}' exists in the fellowship.");

            _members.Remove(characterToRemove);
            return this;
        }

        public override string ToString()
            => _members.Aggregate("Fellowship of the Ring Members:\n", (current, member) => current + (member + "\n"));

        public Either<Error, Fellowship> MoveTo(Region destination, Logger logger, params Name[] names)
        {
            var errors = _members
                .Where(character => ContainsCharacter(names, character))
                .Select(character => character.Move(destination, logger))
                .Lefts()
                .ToList();

            return errors.Count != 0
                ? errors[0]
                : this;
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