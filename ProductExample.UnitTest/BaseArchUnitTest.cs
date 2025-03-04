using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using System.Reflection;
using System.Linq;
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
        
    protected static readonly Architecture DynamicArchitecture = new ArchLoader()
        .LoadAssemblies(GetProductExampleAssemblies())
        .Build();

    private static System.Reflection.Assembly[] GetProductExampleAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assembly.GetName().Name?.StartsWith("ProductExample") is true)
            .ToArray();
    }
    
    protected readonly IObjectProvider<IType> ProductATypes =
        Types().That().ResideInNamespace("ProductExample.Product.A", true);

    protected readonly IObjectProvider<IType> ProductBTypes =
        Types().That().ResideInNamespace("ProductExample.Product.B", true);
        
    protected readonly IObjectProvider<IType> CoreTypes =
        Types().That().ResideInNamespace("ProductExample.Core", true);
        
    protected readonly IObjectProvider<IType> ExtensionsTypes =
        Types().That().ResideInNamespace("ProductExample.Extensions", true);
}