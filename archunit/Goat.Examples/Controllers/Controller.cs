using Goat.Examples.Models;
using Microsoft.AspNetCore.Mvc;

namespace Goat.Examples.Controllers
{
    [ApiController]
    public class Controller
    {
        public ApiResponse<int> Matching() => new ApiResponse<int>(42);

        public void NotMatching()
        {
        }

        public int Universe() => 42;
    }
}