using LanguageExt;
using LanguageExt.Common;

namespace LordOfTheRings.Domain
{
    public sealed class Damage : IEquatable<Damage>
    {
        private readonly int _value;
        private Damage(int value) => _value = value;

        public static Either<Error, Damage> Parse(int value)
            => value < 0
                ? Error.New("A damage can not be negative")
                : new Damage(value);

        public override string ToString() => _value.ToString();

        #region Equality operators

        public bool Equals(Damage? other) => _value == other?._value;
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Damage other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Damage a, Damage b) => a._value == b._value;
        public static bool operator !=(Damage a, Damage b) => !(a == b);
        public static bool operator ==(Damage a, int b) => a._value == b;
        public static bool operator !=(Damage a, int b) => !(a == b);

        #endregion
    }

    public static class DamageExtensions
    {
        public static Either<Error, Damage> ToDamage(this int value) => Damage.Parse(value);
    }
}