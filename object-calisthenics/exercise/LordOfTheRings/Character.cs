namespace LordOfTheRings
{
    public sealed class Character
    {
        public string N { get; set; }
        public string R { get; set; }
        public string W { get; set; }
        public string C { get; set; } = "Shire";

        public override string ToString() => $"{N} ({R}) with {W} in {C}";
    }
}