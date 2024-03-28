using System.Text;
using LanguageExt;

namespace GoatNumerals
{
    using static LanguageExt.Prelude;

    public static class GoatNumeralsConverter
    {
        private static readonly Dictionary<int, string> IntToGoatNumerals = new()
        {
            {1000, "🐐"},
            {900, "Meu🐐"},
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
        {
            if (number != 0)
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

                return Some(goatNumerals.ToString());
            }
            else
            {
                return None;
            }
        }
    }
}