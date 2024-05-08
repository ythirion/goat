namespace Goat.Architecture.Domain
{
    public interface IGoatRepository
    {
        Task<Goat> Save(Goat goat);
    }
}