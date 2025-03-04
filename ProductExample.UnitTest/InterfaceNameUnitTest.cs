using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

public class InterfaceNameUnitTest : BaseArchUnitTest
{
    [Fact]
    public void Interfaces_ShouldStartWith_LetterI()
    {
        var rule = Interfaces()
            .Should().HaveNameStartingWith("I");

        rule.Because("Todas as interfaces devem começar com a letra 'I' para seguir as convenções de nomenclatura")
            .Check(DynamicArchitecture);
    }
}