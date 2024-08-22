namespace LordOfTheRings;
public static class ResultExtensions
{
    public static void HandleResult(this Result result)
    {
        if (!result.IsSuccess)
        {
            Console.WriteLine($"{result.Message}");
        }
    }

    public static void HandleResult(this Result result, List<string> names, string region)
    {
        if (!result.IsSuccess)
        {
            Console.WriteLine($"{result.Message}");

            return;
        }

        foreach (string name in names)
        {
            Console.WriteLine($"{name} moved to {region}.");
        }
    }
}