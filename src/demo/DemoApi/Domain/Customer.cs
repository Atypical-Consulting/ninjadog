using DemoApi.Domain.Common;
using Ninjadog;

namespace DemoApi.Domain;

[NinjadogModel]
public class Customer
{
    public CustomerId Id { get; init; } = CustomerId.From(Guid.NewGuid());

    public required Username Username { get; init; }

    public required FullName FullName { get; init; }

    public required EmailAddress Email { get; init; }

    public required DateOfBirth DateOfBirth { get; init; }
}
