using ArchUnitNET.xUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

public class ControllerAuthUnitTest : BaseArchUnitTest
{
    [Fact]
    public void Controller_ShouldAlwaysUse_AuthorizeAttribute()
    {
        var rule = Classes()
            .That().AreAssignableTo(typeof(ControllerBase))
            .Should().HaveAnyAttributes(typeof(AuthorizeAttribute))
            .AndShould().NotHaveAnyAttributes(typeof(AllowAnonymousAttribute));

        rule.Because("Todos os controllers precisam ter a configuração de Auth, e não podem ter AllowAnonymous")
            .WithoutRequiringPositiveResults()
            .Check(DynamicArchitecture);
    }

    [Fact]
    public void ControllerActions_ShouldNeverUse_AllowAnonymousAttribute()
    {
        var controllers = Classes().That().AreAssignableTo(typeof(ControllerBase));
        var httpAnnotations = new[]
        {
            typeof(HttpGetAttribute),
            typeof(HttpPostAttribute),
            typeof(HttpPutAttribute),
            typeof(HttpPatchAttribute),
            typeof(HttpDeleteAttribute),
            typeof(HttpOptionsAttribute)
        };

        var rule = MethodMembers().That()
            .AreDeclaredIn(controllers).And()
            .HaveAnyAttributes(httpAnnotations)
            .Should()
            .NotHaveAnyAttributes(typeof(AllowAnonymousAttribute));

        rule.Because("Todos os endpoints não podem ter AllowAnonymous")
            .Check(DynamicArchitecture);
    }
}