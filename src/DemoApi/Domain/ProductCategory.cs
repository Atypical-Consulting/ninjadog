using DemoLibrary;

namespace DemoApi.Domain;

[NinjadogModel]
public partial class ProductCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
