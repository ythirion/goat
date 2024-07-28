using LordOfTheRings.Domain;

namespace LordOfTheRings.Tests.Domain
{
    public class CharacterBuilder
    {
        private readonly string _name;
        private Race? _race;
        private string? _weapon;
        private Region? _region;
        private CharacterBuilder(string name) => _name = name;

        public static CharacterBuilder ACharacter(string name) => new(name);
        public CharacterBuilder Hobbit() => Act(() => _race = Race.Hobbit);
        public CharacterBuilder Wizard() => Act(() => _race = Race.Wizard);
        public CharacterBuilder Dwarf() => Act(() => _race = Race.Dwarf);
        public CharacterBuilder With(string weapon) => Act(() => _weapon = weapon);
        public CharacterBuilder In(Region region) => Act(() => _region = region);

        private CharacterBuilder Act(Action action)
        {
            action();
            return this;
        }

        public Character Build() => new(
            _name.ToName(),
            _race ?? Race.Hobbit,
            new Weapon((_weapon ?? "Sting").ToName(), Faker.RandomNumber.Next(0, 200).ToDamage()),
            _region ?? Region.Shire
        );
    }
}