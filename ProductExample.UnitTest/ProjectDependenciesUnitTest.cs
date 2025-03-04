using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

public class ProjectDependenciesUnitTest : BaseArchUnitTest
{

    [Fact]
    public void ProjectB_ShouldOnlyDependOn_CoreAndExtensions()
    {
        var allowedDependenciesForB = Types()
            .That().ResideInNamespace("ProductExample.Core", true)
            .Or().ResideInNamespace("ProductExample.Extensions", true);
        
        var rule = Types().That().Are(ProductBTypes)
            .Should().OnlyDependOnTypesThat().Are(allowedDependenciesForB);

        rule.Because("Project B should only depend on Core and Extensions")
            .Check(Architecture);
    }

    [Fact]
    public void Core_ShouldNotDependOn_AnyOtherProject()
    {
        var forbiddenDependenciesForCore = Types()
            .That().ResideInNamespace("ProductExample.Extensions", true)
            .Or().ResideInNamespace("ProductExample.Product.A", true)
            .Or().ResideInNamespace("ProductExample.Product.B", true);

        var rule = Types().That().Are(CoreTypes)
            .Should().NotDependOnAny(forbiddenDependenciesForCore);

        rule.Because("Core should not depend on any other project")
            .Check(Architecture);
    }

    [Fact]
    public void Extensions_ShouldOnlyDependOn_Core()
    {
        var allowedDependenciesForExtensions = Types()
            .That().ResideInNamespace("ProductExample.Core", true);
            
        var rule = Types().That().Are(ExtensionsTypes)
            .Should().OnlyDependOnTypesThat().Are(allowedDependenciesForExtensions);

        rule.Because("Extensions should only depend on Core")
            .Check(Architecture);
    }

    [Fact]
    public void NoCyclicDependencies_BetweenProjects()
    {
        var rule = Types().That().Are(ProductATypes)
            .Should().NotDependOnAny(ProductBTypes);

        rule.Because("Cyclic dependencies between products are not allowed")
            .Check(Architecture);
    }
}