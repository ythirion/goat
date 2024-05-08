using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Goat.Examples.Tests
{
    public class NamingConvention
    {
        [Fact]
        public void ServicesShouldBeSuffixedByService() =>
            Classes()
                .That()
                .ResideInNamespace("Services", true).Should()
                .HaveNameEndingWith("Service")
                .Check();

        [Fact]
        public void CommandHandlersShouldBeSuffixedByCommandHandler() =>
            Classes()
                .That()
                .ImplementInterface(typeof(ICommandHandler<>)).Should()
                .HaveNameEndingWith("CommandHandler")
                .Check();
    }
}