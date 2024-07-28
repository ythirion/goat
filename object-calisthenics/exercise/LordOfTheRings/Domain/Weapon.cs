namespace LordOfTheRings.Domain
{
    public class Weapon
    {
        public Name Name { get; set; }
        public Damage Damage { get; set; }

        public override string ToString() => Name.ToString();
    }
}