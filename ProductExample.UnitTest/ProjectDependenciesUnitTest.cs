using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

public class ProjectDependenciesUnitTest : BaseArchUnitTest
{
    [Fact]
    public void ProductA_ShouldOnlyDependOn_CoreAndExtensions()
    {
        var rule = Types().That().Are(ProductATypes)
            .Should().OnlyDependOnTypesThat().Are(AllowedDependencies);

        rule.Check(Architecture);
    }

    [Fact]
    public void ProductB_ShouldOnlyDependOn_CoreAndExtensions()
    {
        var rule = Types().That().Are(ProductBTypes)
            .Should().OnlyDependOnTypesThat().Are(AllowedDependencies);

        rule.Check(Architecture);
    }
}