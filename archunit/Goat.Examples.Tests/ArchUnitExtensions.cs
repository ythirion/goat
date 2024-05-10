using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Goat.Examples.Services;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Goat.Examples.Tests
{
    public static class ArchUnitExtensions
    {
        private static readonly Architecture Architecture =
            new ArchLoader()
                .LoadAssemblies(typeof(ServiceNotCompliant).Assembly)
                .Build();

        public static GivenTypesConjunction TypesInAssembly() =>
            Types().That().Are(Architecture.Types);

        public static void Check(this IArchRule rule) => rule.Check(Architecture);
    }
}