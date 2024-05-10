using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Goat.Examples.Tests
{
    public class Annotations
    {
        private const string UseConfigFile = "You should use config file instead of ExcludeFromCodeCoverageAttribute";
        private static readonly Type ExcludeFromCoverage = typeof(ExcludeFromCodeCoverageAttribute);

        [Fact]
        public void AnnotatedClassesShouldResideInAGivenNamespace() =>
            Classes().That()
                .HaveAnyAttributes(typeof(ApiControllerAttribute))
                .Should()
                .ResideInNamespace("Controllers", true)
                .Check();

        [Fact]
        public void CoverageAttributesShouldNotBeUsedOnClasses() =>
            Classes()
                .That().HaveAnyAttributes(ExcludeFromCoverage).Should()
                .NotExist()
                .Because(UseConfigFile)
                .Check();

        [Fact]
        public void CoverageAttributesShouldNotBeUsedOnMethods() =>
            MethodMembers()
                .That().HaveAnyAttributes(ExcludeFromCoverage).Should()
                .NotExist()
                .Because(UseConfigFile)
                .Check();

        [Fact]
        public void CoverageAttributesShouldNotBeUsedOnProperties() =>
            PropertyMembers()
                .That()
                .HaveAnyAttributes(ExcludeFromCoverage).Should()
                .NotExist()
                .Because(UseConfigFile)
                .Check();
    }
}