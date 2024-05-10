namespace Goat.Domain
{
    public interface IGoatRepository
    {
        Task<Goat> Save(Goat goat);
    }
}