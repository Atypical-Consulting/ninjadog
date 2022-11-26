using ValueOf;

namespace DemoApi.Domain.Common;

public class ProductId : ValueOf<Guid, ProductId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Product Id cannot be empty", nameof(ProductId));
        }
    }
}
