using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

public class ProjectDependenciesUnitTest : BaseArchUnitTest
{
    [Fact]
    public void Core_ShouldNotDependOn_AnyOtherProject()
    {
        var forbiddenDependenciesForCore = Types()
            .That().ResideInNamespace("ProductExample", true)
            .And().DoNotResideInNamespace("ProductExample.Core", true);

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
            .Check(DynamicArchitecture);
    }

    [Fact]
    public void NoCyclicDependencies_BetweenProjectsDynamically()
    {
        var productNamespaces = DynamicArchitecture.Types
            .Where(type => type.FullName.StartsWith("ProductExample.Product."))
            .Select(type => type.Namespace)
            .Distinct()
            .ToList();
        
        foreach (var currentProductNamespace in productNamespaces)
        {
            foreach (var otherProductNamespace in productNamespaces)
            {
                var isTheSameNamespace = Equals(currentProductNamespace, otherProductNamespace);
                if (isTheSameNamespace)
                    continue;

                var currentProductTypes = Types().That().ResideInNamespace(currentProductNamespace.Name, true);
                var otherProductTypes = Types().That().ResideInNamespace(otherProductNamespace.Name, true);

                var rule = Types().That().Are(currentProductTypes)
                    .Should().NotDependOnAny(otherProductTypes);

                rule.Because($"Produto {currentProductNamespace.Name} não deve depender de {otherProductNamespace.Name}")
                    .Check(DynamicArchitecture);
            }
        }
    }
}