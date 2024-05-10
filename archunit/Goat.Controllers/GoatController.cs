using Goat.Repositories;
using Goat.UseCases;

namespace Goat.Controllers
{
    public class GoatController(
        FeedGoat feedGoat,
        GoatRepository goatRepository)
    {
    }
}