namespace LordOfTheRings.Domain;

public sealed class Character : IEquatable<Character>
{
    public Identity Identity { get; }
    public State CurrentState { get; set; }

    public Character(Identity identity, State currentState)
    {
        Identity = identity;
        CurrentState = currentState;
    }
    
    public bool InMordor() => CurrentState.InMordor();

    public bool Equals(Character? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Identity.Equals(other.Identity) && CurrentState.Equals(other.CurrentState);
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Character other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Identity, CurrentState);
}