namespace Goat.Examples.Models
{
    public record ApiResponse<TData>(TData Data, ApiError[]? Errors = null)
    {
    }
}