using DemoApi.Domain.Common;
using Ninjadog;

namespace DemoApi.Domain;

[NinjadogModel]
public class Product
{
    public ProductId Id { get; init; } = ProductId.From(Guid.NewGuid());

    public required ProductName Name { get; init; }

    public required ProductDescription Description { get; init; }

    public required Price Price { get; init; }
}
