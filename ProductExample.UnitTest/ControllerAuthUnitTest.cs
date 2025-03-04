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
            .That().AreAssignableTo(typeof(ControllerBase)).And().DoNotHaveName(nameof(ControllerBase))
            .Should().HaveAnyAttributes(typeof(AuthorizeAttribute))
            .AndShould().NotHaveAnyAttributes(typeof(AllowAnonymousAttribute));

        rule.Because("Todos os controllers precisam ter a configuração de Auth, e não podem ter AllowAnonymous")
            .WithoutRequiringPositiveResults()
            .Check(DynamicArchitecture);
    }
    //TODO: Criar testes para as Actions
}