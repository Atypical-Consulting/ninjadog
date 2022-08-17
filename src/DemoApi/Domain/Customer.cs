using DemoApi.Domain.Common;
using DemoLibrary;

namespace DemoApi.Domain;

[NinjadogModel]
public class Customer
{
    public CustomerId Id { get; init; } = CustomerId.From(Guid.NewGuid());

    public Username Username { get; init; } = default!;

    public FullName FullName { get; init; } = default!;

    public EmailAddress Email { get; init; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
}
