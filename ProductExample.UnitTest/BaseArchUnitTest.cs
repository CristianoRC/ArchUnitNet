using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ProductExample.UnitTest;

[Trait("category", "arch")]
public abstract class BaseArchUnitTest
{
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            typeof(Core.Setup).Assembly,
            typeof(Extensions.Setup).Assembly,
            typeof(Product.A.Setup).Assembly,
            typeof(Product.B.Setup).Assembly)
        .Build();

    protected readonly IObjectProvider<IType> ProductATypes =
        Types().That().ResideInNamespace("ProductExample.Product.A", true);

    protected readonly IObjectProvider<IType> ProductBTypes =
        Types().That().ResideInNamespace("ProductExample.Product.B", true);

    protected readonly IObjectProvider<IType> AllowedDependencies =
        Types().That().ResideInNamespace("ProductExample.Core", true)
            .Or().ResideInNamespace("ProductExample.Extensions", true);
}