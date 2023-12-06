namespace Ninjadog.Tests;

[UsesVerify]
public class NinjadogModelSnapshotTests
{
    [Fact]
    public Task Test1()
    {
        const string source = @"
using System;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;
using Ninjadog;

[NinjadogModel]
public class Customer
{
    public CustomerId Id { get; init; } = CustomerId.From(Guid.NewGuid());

    public Username Username { get; init; } = default!;

    public FullName FullName { get; init; } = default!;

    public EmailAddress Email { get; init; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
}

public class CustomerId : ValueOf<Guid, CustomerId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException(""Customer Id cannot be empty"", nameof(CustomerId));
        }
    }
}

public class Username : ValueOf<string, Username>
{
    private static readonly Regex UsernameRegex =
        new(""^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$"", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!UsernameRegex.IsMatch(Value))
        {
            var message = $""{Value} is not a valid username"";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Username), message)
            });
        }
    }
}


public class FullName : ValueOf<string, FullName>
{
    private static readonly Regex FullNameRegex =
        new(""^[a-z ,.'-]+$"", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!FullNameRegex.IsMatch(Value))
        {
            var message = $""{Value} is not a valid full name"";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(FullName), message)
            });
        }
    }
}

public class EmailAddress : ValueOf<string, EmailAddress>
{
    private static readonly Regex EmailRegex =
        new(""^[\\w!#$%&’*+/=?`{|}~^-]+(?:\\.[\\w!#$%&’*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$"",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!EmailRegex.IsMatch(Value))
        {
            var message = $""{Value} is not a valid email address"";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(EmailAddress), message)
            });
        }
    }
}

public class DateOfBirth : ValueOf<DateOnly, DateOfBirth>
{
    protected override void Validate()
    {
        if (Value > DateOnly.FromDateTime(DateTime.Now))
        {
            const string message = ""Your date of birth cannot be in the future"";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(DateOfBirth), message)
            });
        }
    }
}";

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task Test2()
    {
        const string source = @"
using System;
using Ninjadog;

[NinjadogModel]
public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Username { get; init; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime DateOfBirth { get; init; } = default!;
}";

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task Test3()
    {
        const string source = @"
using System;
using Ninjadog;

[NinjadogModel]
public class Customer
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Username { get; init; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime DateOfBirth { get; init; } = default!;
}";

        return TestHelper.Verify(source);
    }
}

