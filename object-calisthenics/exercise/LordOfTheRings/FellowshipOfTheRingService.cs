using LanguageExt;
using LanguageExt.Common;
using LordOfTheRings.Domain;

namespace LordOfTheRings
{
    public class FellowshipOfTheRingService(Logger logger)
    {
        private Fellowship _fellowship = new();

        public Either<Error, Unit> AddMember(Character character)
            => _fellowship
                .AddMember(character)
                .Do(f => _fellowship = f)
                .Map(_ => Unit.Default);

        public Either<Error, Unit> RemoveMember(string name)
            => name.ToName()
                .Bind(n => _fellowship.Remove(n))
                .Match(f => _fellowship = f, err => logger(err.Message));

        public Either<Error, Unit> MoveMembersToRegion(List<string> memberNames, string region)
            => _fellowship.MoveTo(
                    region.ToRegion(),
                    logger,
                    memberNames.Map(m => m.ToName()).Rights().ToArray()
                )
                .Match(f => _fellowship = f, err => logger(err.Message));

        public void PrintMembersInRegion(string region) =>
            _fellowship.PrintMembersInRegion(region.ToRegion(), logger);

        public override string ToString() => _fellowship.ToString();
    }
}