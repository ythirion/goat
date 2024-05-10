using ArchUnitNET.Fluent.Syntax.Elements.Types.Classes;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Goat.Examples.Tests
{
    public class MethodReturnTypes
    {
        [Fact]
        public void ControllersPublicMethodShouldOnlyReturnApiResponse() =>
            MethodMembers().That()
                .ArePublic().And()
                .AreNoConstructors().And()
                .AreDeclaredIn(Controllers()).Should()
                .HaveReturnType(typeof(ApiResponse<>))
                .Check();

        private static GivenClassesConjunction Controllers() =>
            Classes().That().HaveAnyAttributes(typeof(ApiControllerAttribute));
    }
}