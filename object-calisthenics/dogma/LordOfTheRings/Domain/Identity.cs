namespace LordOfTheRings.Domain;

public sealed class Identity
{
    private readonly CharacterName _name;
    private readonly Race _race;

    public Identity(CharacterName name, Race race)
    {
        _name = name;
        _race = race;
    }

    public bool Is(CharacterName name) => _name.Equals(name);

    public override string ToString() => $"{_name} ({_race})";
}