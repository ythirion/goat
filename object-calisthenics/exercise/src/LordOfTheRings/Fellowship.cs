namespace LordOfTheRings;
public sealed class Fellowship
{
    private readonly List<Character> _members = new();

    public Result AddMember(Character character)
    {
        if (character == null)
        {
            return Result.Failure("Character cannot be null.");
        }

        if (_members.Exists(m => m.Name == character.Name))
        {
            return Result.Failure($"A character with the name '{character.Name}' already exists in the fellowship.");
        }

        _members.Add(character);

        return Result.Success();
    }

    public Result<Character> GetCharacterByName(string name)
    {
        var character = _members.Find(c => c.Name == name);

        return character != null
            ? Result<Character>.Success(character)
            : Result<Character>.Failure($"No character with the name '{name}' exists in the fellowship.");
    }

    public List<Character> GetMembersInRegion(string region)
    {
        return _members.Where(c => c.CurrentRegion == region).ToList();
    }

    public Result MoveMembersToRegion(List<string> memberNames, string region)
    {
        foreach (string name in memberNames)
        {
            Result<Character> characterResult = GetCharacterByName(name);

            if (!characterResult.IsSuccess)
            {
                return characterResult;
            }

            var moveResult = characterResult.Value.MoveToRegion(region);

            if (!moveResult.IsSuccess)
            {
                return moveResult;
            }
        }

        return Result.Success();
    }

    public Result RemoveMember(string name)
    {
        Result<Character> characterResult = GetCharacterByName(name);

        if (!characterResult.IsSuccess)
        {
            return characterResult;
        }

        _members.Remove(characterResult.Value);

        return Result.Success();
    }

    public override string ToString()
    {
        return string.Join("\n", _members.Select(m => $"{m.Name} ({m.Race}) with {m.Weapon.Name} in {m.CurrentRegion}"));
    }
}