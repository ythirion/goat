using LanguageExt;
using LanguageExt.Common;
using Characters = LanguageExt.Seq<LordOfTheRings.Domain.Character>;

namespace LordOfTheRings.Domain
{
    public sealed class Fellowship
    {
        private Characters _members = [];

        public Either<Error, Fellowship> AddMember(Character character)
            => _members.Find(c => c == character)
                .Map(_ => Error.New("A character with the same name already exists in the fellowship."))
                .ToEither(this)
                .Swap()
                .Do(_ => _members = _members.Add(character));

        public Either<Error, Fellowship> Remove(Name name)
            => _members.Find(character => character.HasName(name))
                .ToEither(Error.New($"No character with the name '{name}' exists in the fellowship."))
                .Do(characterToRemove => _members = _members.Filter(c => c != characterToRemove))
                .Map(_ => this);

        public override string ToString()
            => _members.Fold("Fellowship of the Ring Members:\n",
                (current, member) => current + (member + "\n")
            );

        public Either<Error, Fellowship> MoveTo(Region destination, Logger logger, params Name[] names)
        {
            var errors = _members
                .Filter(character => ContainsCharacter(names, character))
                .Map(character => character.Move(destination, logger))
                .Lefts();

            return errors.Count != 0
                ? errors[0]
                : this;
        }

        private static bool ContainsCharacter(Name[] names, Character character)
            => names.Exists(character.HasName);

        public void PrintMembersInRegion(Region region, Logger logger)
        {
            var charactersInRegion = _members.Filter(m => m.IsIn(region));
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