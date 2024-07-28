using FsCheck;
using LordOfTheRings.Domain;

namespace LordOfTheRings.Tests.Domain
{
    public class DamageShould
    {
        [Fact]
        public void Parse_Positive_Int()
            => Prop.ForAll(
                    Arb.Default.PositiveInt(),
                    validDamage => Damage.Parse(validDamage.Get) == validDamage.Get)
                .QuickCheckThrowOnFailure();

        [Fact]
        public void Not_Parse_Negative_Int()
            => Prop.ForAll(
                    Arb.Default.NegativeInt(),
                    invalidDamage => IsParsingFailFor(invalidDamage.Get))
                .QuickCheckThrowOnFailure();

        private static bool IsParsingFailFor(int invalidDamage)
        {
            try
            {
                Damage.Parse(invalidDamage);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
    }
}