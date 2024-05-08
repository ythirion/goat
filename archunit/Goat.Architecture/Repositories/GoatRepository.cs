using Goat.Architecture.Domain;

namespace Goat.Architecture.Repositories
{
    public class GoatRepository : IGoatRepository
    {
        public Task<Domain.Goat> Save(Domain.Goat goat)
        {
            throw new NotImplementedException();
        }
    }
}