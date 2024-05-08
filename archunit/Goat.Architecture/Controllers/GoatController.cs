using Goat.Architecture.Domain;
using Goat.Architecture.UseCases;

namespace Goat.Architecture.Controllers
{
    public class GoatController
    {
        private readonly FeedGoat _feedGoat;
        private readonly IGoatRepository _goatRepository;

        public GoatController(
            FeedGoat feedGoat,
            IGoatRepository goatRepository)
        {
            _feedGoat = feedGoat;
            _goatRepository = goatRepository;
        }
    }
}