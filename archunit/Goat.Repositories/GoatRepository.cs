using Goat.Domain;

namespace Goat.Repositories
{
    public class GoatRepository : IGoatRepository
    {
        public Task<Domain.Goat> Save(Domain.Goat goat)
        {
            throw new NotImplementedException();
        }
    }
}