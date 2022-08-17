using DemoLibrary;

namespace DemoApi.Domain;

[NinjadogModel]
public partial class ProductCategory
{
    public static readonly ProductCategory Fruits = new("Fruits");
    public static readonly ProductCategory Dairy = new("Dairy");
    public static readonly ProductCategory Vegetables = new("Vegetables");

    public string Name { get; }

    private ProductCategory(string name)
    {
        Name = name;
    }
}
