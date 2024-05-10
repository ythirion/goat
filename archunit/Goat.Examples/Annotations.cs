using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Goat.Examples
{
    [ExcludeFromCodeCoverage]
    public class Feed
    {
        [ExcludeFromCodeCoverage] public string Name { get; set; }

        [ExcludeFromCodeCoverage]
        public void Again()
        {
        }
    }

    [ApiController]
    public class GoatController
    {
    }
}