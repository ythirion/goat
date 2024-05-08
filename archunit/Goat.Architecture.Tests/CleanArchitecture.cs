using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Goat.Architecture.Controllers;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Goat.Architecture.Tests
{
    public class CleanArchitecture
    {
        private static readonly ArchUnitNET.Domain.Architecture Architecture =
            new ArchLoader()
                .LoadAssemblies(typeof(GoatController).Assembly)
                .Build();

        private static GivenTypesConjunction TypesIn(string @namespace) =>
            Types().That()
                .ResideInNamespace(@namespace, true);

        private static GivenTypesConjunctionWithDescription Controllers() =>
            TypesIn("Controllers").As("Interface Adapters");

        private static GivenTypesConjunctionWithDescription UseCases() =>
            TypesIn("UseCases").As("Application Business Rules");

        private static GivenTypesConjunctionWithDescription Frameworks_Drivers() =>
            TypesIn("Repositories").As("Framework & Drivers");

        private static GivenTypesConjunctionWithDescription Entities() =>
            TypesIn("Domain").As("Enterprise Business Rules");

        [Fact(DisplayName = "Lower layers can not depend on outer layers")]
        public void DependencyRule() =>
            Entities().Should()
                .OnlyDependOn(Entities())
                .And(
                    UseCases().Should()
                        .NotDependOnAny(Controllers()).AndShould()
                        .NotDependOnAny(Frameworks_Drivers())
                )
                .And(
                    Controllers().Should()
                        .NotDependOnAny(Frameworks_Drivers())
                )
                .Check(Architecture);
    }
}