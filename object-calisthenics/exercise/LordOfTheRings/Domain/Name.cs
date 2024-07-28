using LanguageExt;
using LanguageExt.Common;

namespace LordOfTheRings.Domain
{
    public sealed class Name : IEquatable<Name>
    {
        private readonly string _value;
        private Name(string value) => _value = value;

        public static Either<Error, Name> Parse(string value)
            => string.IsNullOrWhiteSpace(value)
                ? Error.New("A name can not be empty.")
                : new Name(value);

        public override string ToString() => _value;

        #region Equality operators

        public bool Equals(Name? other) => _value == other?._value;
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Name other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Name a, Name b) => a._value == b._value;
        public static bool operator !=(Name a, Name b) => !(a == b);
        public static bool operator ==(Name a, string b) => a._value == b;
        public static bool operator !=(Name a, string b) => !(a == b);

        #endregion
    }

    public static class NameExtensions
    {
        public static Either<Error, Name> ToName(this string value) => Name.Parse(value);
    }
}