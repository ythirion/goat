using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit;

namespace Goat.Architecture.Tests
{
    public class SplitArchitecture
    {
        private static readonly ArchUnitNET.Domain.Architecture Architecture =
            new ArchLoader()
                .LoadAssemblies(
                    typeof(Goat.Controllers.GoatController).Assembly,
                    typeof(Goat.UseCases.FeedGoat).Assembly,
                    typeof(Goat.Domain.Goat).Assembly,
                    typeof(Goat.Repositories.GoatRepository).Assembly
                )
                .Build();

        private static GivenTypesConjunction TypesIn(string @namespace) =>
            ArchRuleDefinition.Types().That().ResideInNamespace(@namespace, true);

        private static GivenTypesConjunctionWithDescription Controllers() =>
            TypesIn("Controllers").As("Interface Adapters");

        private static GivenTypesConjunctionWithDescription UseCases() =>
            TypesIn("UseCases").As("Application Business Rules");

        private static GivenTypesConjunctionWithDescription Frameworks_Drivers() =>
            TypesIn("Repositories").As("Framework & Drivers");

        private static GivenTypesConjunctionWithDescription Domain() =>
            TypesIn("Domain").As("Enterprise Business Rules");

        [Fact(DisplayName = "Lower layers can not depend on outer layers")]
        public void CheckRule() => DependencyRule.Check(Architecture);

        private static IArchRule DependencyRule =>
            Domain()
                .Should()
                .OnlyDependOn(Domain())
                .And(
                    UseCases().Should()
                        .NotDependOnAny(Controllers()).AndShould()
                        .NotDependOnAny(Frameworks_Drivers())
                )
                .And(
                    Controllers().Should()
                        .NotDependOnAny(Frameworks_Drivers())
                );
    }
}