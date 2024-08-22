namespace LordOfTheRings;
public sealed class FellowshipOfTheRingService
{
    private readonly Fellowship _fellowship = new();

    public Result AddMember(Character character) => _fellowship.AddMember(character);

    public string GetMembersInRegion(string region)
    {
        List<Character> charactersInRegion = _fellowship.GetMembersInRegion(region);

        if (charactersInRegion.Count == 0)
        {
            return $"No members in {region}";
        }

        var members = charactersInRegion
                      .Select(character => $"{character.Name} ({character.Race}) with {character.Weapon.Name}")
                      .ToList();

        return $"Members in {region}:\n{string.Join("\n", members)}";
    }

    public Result MoveMembersToRegion(List<string> memberNames, string region) => _fellowship.MoveMembersToRegion(memberNames, region);

    public Result RemoveMember(string name) => _fellowship.RemoveMember(name);

    public override string ToString() => "Fellowship of the Ring Members:\r\n" + _fellowship;

    public Result UpdateCharacterWeapon(string name, string newWeaponName, int damage)
    {
        Result<Character> characterResult = _fellowship.GetCharacterByName(name);

        return !characterResult.IsSuccess ? characterResult : characterResult.Value.UpdateWeapon(newWeaponName, damage);
    }
}