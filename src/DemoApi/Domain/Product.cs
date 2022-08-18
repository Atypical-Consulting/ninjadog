using DemoApi.Domain.Common;
using Ninjadog;

namespace DemoApi.Domain;

[NinjadogModel]
public class Product
{
    public CustomerId Id { get; init; } = CustomerId.From(Guid.NewGuid());

    public Username Username { get; init; } = default!;

    public FullName FullName { get; init; } = default!;

    public EmailAddress Email { get; init; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
}
