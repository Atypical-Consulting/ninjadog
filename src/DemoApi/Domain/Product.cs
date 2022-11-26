using DemoApi.Domain.Common;
using Ninjadog;

namespace DemoApi.Domain;

[NinjadogModel]
public class Product
{
    public ProductId Id { get; init; } = ProductId.From(Guid.NewGuid());

    public ProductName Name { get; init; } = default!;

    public Price Price { get; init; } = default!;
}
