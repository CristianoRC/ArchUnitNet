using ArchUnitNET.xUnit;
using ProductExample.Core;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

public class SetupContractUnitTest : BaseArchUnitTest
{
    [Fact]
    public void Setup_ShouldImplement_ISetup()
    {
        var rule = Classes().That().HaveName("Setup")
            .Should().ImplementInterface(typeof(ISetup));

        rule.Because("Todas as classes de Setup devem implementar o contrato ISetup")
            .Check(DynamicArchitecture);
    }
}