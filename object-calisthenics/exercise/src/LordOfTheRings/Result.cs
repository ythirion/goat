namespace LordOfTheRings;
public class Result
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static Result Failure(string error) => new(false, error);

    public static Result Success() => new(true, string.Empty);
}
public sealed class Result<T> : Result
{
    public T Value { get; }

    private Result(T value, bool isSuccess, string message)
        : base(isSuccess, message) => Value = value;

    public new static Result<T> Failure(string message) => new(default, false, message);

    public static Result<T> Success(T value, string message=null) => new(value, true, message);
}