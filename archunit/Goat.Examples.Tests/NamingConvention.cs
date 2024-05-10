using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Goat.Examples.Tests
{
    public class NamingConvention
    {
        [Fact]
        public void AllMethodsShouldStartWithACapitalLetter() =>
            MethodMembers()
                .That().AreNoConstructors()
                .And().DoNotHaveAnyAttributes("SpecialName|CompilerGenerated", true)
                .Should()
                .HaveName(@"^[A-Z]", true)
                .Because("C# convention...")
                .Check();

        [Fact]
        public void InterfacesShouldStartWithI() =>
            Interfaces().Should()
                .HaveName("^I[A-Z].*", useRegularExpressions: true)
                .Because("C# convention...")
                .Check();

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