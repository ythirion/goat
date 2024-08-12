using LanguageExt;
using LanguageExt.Common;
using Characters = LanguageExt.Seq<LordOfTheRings.Domain.Character>;

namespace LordOfTheRings.Domain
{
    public sealed class Fellowship
    {
        private readonly Characters _members;

        public Fellowship() => _members = [];
        private Fellowship(Characters members) => _members = members;
        private Fellowship(Characters allMembers, Characters updated) => _members = allMembers + updated;

        public Either<Error, Fellowship> AddMember(Character character)
            => _members.Find(c => c == character)
                .Map(_ => Error.New("A character with the same name already exists in the fellowship."))
                .ToEither(defaultLeftValue: new Fellowship(_members.Add(character)))
                .Swap();

        public Either<Error, Fellowship> Remove(Name name)
            => _members.Find(character => character.HasName(name))
                .ToEither(defaultLeftValue: Error.New($"No character with the name '{name}' exists in the fellowship."))
                .Map(characterToRemove => new Fellowship(_members.Filter(c => c != characterToRemove)));

        public override string ToString()
            => _members.Fold("Fellowship of the Ring Members:\n",
                (current, member) => current + (member + "\n")
            );

        public Either<Error, Fellowship> MoveTo(Region destination, Logger logger, params Name[] names)
        {
            var membersToUpdate = _members.Filter(character => ContainsCharacter(names, character));
            var results = membersToUpdate.Map(character => character.Move(destination, logger));
            var errors = results.Lefts();

            return errors.Count != 0
                ? errors[0]
                : new Fellowship(_members.Filter(c => !membersToUpdate.Contains(c)), results.Rights());
        }

        private static bool ContainsCharacter(Name[] names, Character character)
            => names.Exists(character.HasName);

        public void PrintMembersInRegion(Region region, Logger printer)
        {
            var charactersInRegion = _members.Filter(m => m.IsIn(region));
            if (charactersInRegion.Count == 0)
            {
                printer($"No members in {region}");
                return;
            }

            printer($"Members in {region}:");

            charactersInRegion
                .ToList()
                .ForEach(character => printer(character.ToStringWithoutRegion()));
        }
    }
}