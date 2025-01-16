namespace LordOfTheRings.Tests
{
    public class CharacterizationTest : IDisposable
    {
        private readonly StringWriter _console = new();

        public CharacterizationTest() => Console.SetOut(_console);

        [Fact]
        public Task Program_Should_Run_Same()
        {
            App.App.Run();
            return Verify(_console.ToString());
        }

        public void Dispose()
        {
            _console.Dispose();
            Console.SetOut(Console.Out);
        }
    }
}