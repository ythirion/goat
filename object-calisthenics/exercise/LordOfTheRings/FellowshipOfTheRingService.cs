using System.Collections;

namespace LordOfTheRings
{
    public class FellowshipOfTheRingService
    {
        private Fellowship fellowship = new();

        public void AddMember(Character character)
        {
            fellowship.AddMember(character);
        }

        public void RemoveMember(Name name)
        {
            fellowship.RemoveMember(name);
        }
        
        public void UpdateCharacterWeapon(Name name, Name newWeapon, Damage damage)
        {
            foreach (var character in fellowship.Members.Where(character => character.Name == name))
            {
                character.ChangeWeapon(new Weapon(newWeapon, damage));
                break;
            }
        }

        public void MoveMembersToRegion(List<Name> memberNames, Region region)
        {
            fellowship.MoveMembersToRegion(memberNames, region);
        }

        public void PrintMembersInRegion(Region region)
        {
            var charactersInRegion = fellowship.GetMembersInRegion(region);
            if (charactersInRegion.Any())
            {
                Console.WriteLine($"Members in {region}:");
                foreach (var character in charactersInRegion)
                {
                    Console.WriteLine($"{character.Name} ({character.Race}) with {character.Weapon.Name}");
                }
            }
            else if (charactersInRegion.Count == 0)
            {
                Console.WriteLine($"No members in {region}");
            }
        }

        public override string ToString()
        {
            var result = "Fellowship of the Ring Members:\n";
            foreach (var member in fellowship.Members)
            {
                result += $"{member.Name} ({member.Race}) with {member.Weapon.Name} in {member.Region}" + "\n";
            }
            return result;
        }
    }
}