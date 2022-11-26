using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace DemoApi.Domain.Common;

public class Price : ValueOf<decimal, Price>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            const string message = $"{nameof(Price)} cannot be negative";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Price), message),
            });
        }
    }
}
