using LanguageExt;
using LanguageExt.Common;
using LordOfTheRings.Domain;

namespace LordOfTheRings
{
    public class FellowshipOfTheRingService(Logger logger)
    {
        private readonly Fellowship _fellowship = new();

        public Either<Error, Unit> AddMember(Character character)
            => _fellowship.AddMember(character).Map(_ => Unit.Default);

        public Either<Error, Unit> RemoveMember(string name)
            => name.ToName()
                .Bind(n => _fellowship.Remove(n))
                .Match(_ => { }, err => logger(err.Message));

        public Either<Error, Unit> MoveMembersToRegion(List<string> memberNames, string region)
            => _fellowship.MoveTo(
                region.ToRegion(),
                logger,
                memberNames.Select(m => m.ToName()).Rights().ToArray()
            ).Match(_ => { }, err => logger(err.Message));

        public void PrintMembersInRegion(string region) =>
            _fellowship.PrintMembersInRegion(region.ToRegion(), logger);

        public override string ToString() => _fellowship.ToString();
    }
}