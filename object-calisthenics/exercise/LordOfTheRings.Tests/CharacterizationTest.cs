namespace LordOfTheRings.Tests
{
    public class CharacterizationTest
    {
        private readonly StringWriter _console;

        public CharacterizationTest()
        {
            _console = new StringWriter();
            Console.SetOut(_console);
        }

        [Fact]
        public Task Program_Should_Run_Same()
        {
            App.App.Run();
            return Verify(_console.ToString());
        }
    }
}