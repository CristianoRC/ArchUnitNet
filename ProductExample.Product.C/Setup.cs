using ProductExample.Core;

namespace ProductExample.Product.C;

public class Setup : ISetup
{
    public B.Setup ExampleB { get; set; }
    public void Configure()
    {
        throw new NotImplementedException();
    }
}