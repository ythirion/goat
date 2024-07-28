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
                    validDamage => Damage.Parse(validDamage.Get).IsRight)
                .QuickCheckThrowOnFailure();

        [Fact]
        public void Not_Parse_Negative_Int()
            => Prop.ForAll(
                    Arb.Default.NegativeInt(),
                    invalidDamage => Damage.Parse(invalidDamage.Get).IsLeft)
                .QuickCheckThrowOnFailure();
    }
}