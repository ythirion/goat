using Microsoft.AspNetCore.Mvc;

namespace Goat.Examples
{
    public record ApiError(string Code, string Message);

    public record ApiResponse<TData>(TData Data, ApiError[]? Errors = null);

    [ApiController]
    public class GoatControllerV2
    {
        public ApiResponse<int> Matching() => new ApiResponse<int>(42);

        public void NotMatching()
        {
        }
    }
}