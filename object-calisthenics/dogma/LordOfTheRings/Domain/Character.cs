namespace LordOfTheRings.Domain;

public sealed class Character : IEquatable<Character>
{
    private readonly Identity _identity;
    private readonly State _currentState;

    public Character(Identity identity, State currentState)
    {
        _identity = identity;
        _currentState = currentState;
    }

    public bool InMordor() => _currentState.InMordor();
    public bool Is(CharacterName name) => _identity.Is(name);

    public Character MoveTo(Region region) =>
        new(
            _identity,
            _currentState.MoveTo(region)
        );

    public bool InRegion(Region region) => _currentState.InRegion(region);

    public string ToStringWithoutRegion() => $"{_identity} with {_currentState.ToStringWeapon()}";
    public override string ToString() => $"{ToStringWithoutRegion()} in {_currentState.ToStringRegion()}";

    public bool Equals(Character? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _identity.Equals(other._identity) && _currentState.Equals(other._currentState);
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Character other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(_identity, _currentState);
}