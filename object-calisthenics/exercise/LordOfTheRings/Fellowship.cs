namespace LordOfTheRings;

public class Fellowship()
{
    public List<Character> Members { get; } = new();
    
    public void AddMember(Character character)
    {
        if (character == null)
        {
            throw new ArgumentNullException(nameof(character), "Character cannot be null.");
        }

        if (Members.Any(member => member.Name == character.Name))
        {
            throw new InvalidOperationException(
                "A character with the same name already exists in the fellowship.");
        }

        Members.Add(character);
    }
    
    public void RemoveMember(Name name)
    {
        var characterToRemove = Members.FirstOrDefault(character => character.Name == name);
        if (characterToRemove == null)
        {
            throw new InvalidOperationException($"No character with the name '{name}' exists in the fellowship.");
        }

        Members.Remove(characterToRemove);
    }
    
    public void MoveMembersToRegion(List<Name> memberNames, Region region)
    {
        foreach (var name in memberNames)
        {
            MoveMemberToRegion(name, region);
        }
    }

    private void MoveMemberToRegion(Name name, Region region)
    {
        foreach (var character in Members.Where(character => character.Name == name))
        {
            character.MoveToRegion(region);
        }
    }

    public List<Character> GetMembersInRegion(Region region)
    {
        return Members.Where(character => character.Region == region).ToList();
    }
}
    