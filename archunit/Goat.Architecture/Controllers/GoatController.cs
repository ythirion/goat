using Goat.Architecture.Domain;
using Goat.Architecture.UseCases;

namespace Goat.Architecture.Controllers
{
    public class GoatController(
        FeedGoat feedGoat,
        IGoatRepository goatRepository)
    {
    }
}