namespace LordOfTheRings.Domain
{
    public class Weapon(Name name, Damage damage)
    {
        public override string ToString() => name.ToString();
    }
}