using System.Text;
using LanguageExt;
using static LanguageExt.Prelude;

namespace GoatNumerals
{
    public static class GoatNumeralsConverter
    {
        private const int Min = 1;
        private const int Max = 3999;

        private static readonly Dictionary<int, string> IntToGoatNumerals = new()
        {
            {1000, "🐐"},
            {900, "Meeh🐐"},
            {500, "Baaa"},
            {400, "MeehBaaa"},
            {100, "Meeh"},
            {90, "MehMeeh"},
            {50, "Baa"},
            {40, "MehBaa"},
            {10, "Meh"},
            {9, "MMeh"},
            {5, "Ba"},
            {4, "MBa"},
            {1, "M"}
        };

        public static Option<string> Convert(int number)
            => IsInRange(number) ? ConvertSafely(number) : None;

        private static bool IsInRange(int number) => number is >= Min and <= Max;

        private static string ConvertSafely(int number)
        {
            var goatNumerals = new StringBuilder();
            var remaining = number;

            foreach (var toGoat in IntToGoatNumerals)
            {
                while (remaining >= toGoat.Key)
                {
                    goatNumerals.Append(toGoat.Value);
                    remaining -= toGoat.Key;
                }
            }

            return goatNumerals.ToString();
        }
    }
}