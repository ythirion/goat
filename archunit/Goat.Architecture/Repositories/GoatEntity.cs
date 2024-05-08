namespace Goat.Architecture.Repositories
{
    public record GoatEntity(Guid Id, string Name, IReadOnlyList<string> Powers)
    {
    }
}