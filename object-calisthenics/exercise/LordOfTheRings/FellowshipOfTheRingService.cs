using LordOfTheRings.Domain;

namespace LordOfTheRings
{
    public class FellowshipOfTheRingService(Logger logger)
    {
        private readonly Fellowship _fellowship = new();
        public void AddMember(Character character) => _fellowship.AddMember(character);
        public void RemoveMember(string name) => _fellowship.Remove(name.ToName());

        public void MoveMembersToRegion(List<string> memberNames, string region)
            => _fellowship.MoveTo(
                region.ToRegion(),
                logger,
                memberNames.Select(m => m.ToName()).ToArray()
            );

        public void PrintMembersInRegion(string region) =>
            _fellowship.PrintMembersInRegion(region.ToRegion(), logger);

        public override string ToString() => _fellowship.ToString();
    }
}