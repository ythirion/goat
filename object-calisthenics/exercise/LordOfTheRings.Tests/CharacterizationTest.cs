namespace LordOfTheRings.Tests
{
    public class CharacterizationTest
    {
        private readonly StringWriter _console = new();

        [Fact]
        public Task Program_Should_Run_Same()
        {
            App.App.Run(_console.WriteLine);
            return Verify(_console.ToString());
        }
    }
}