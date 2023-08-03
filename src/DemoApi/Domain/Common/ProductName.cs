using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace DemoApi.Domain.Common;

public class ProductName : ValueOf<string, ProductName>
{
    private static readonly Regex ProductNameRegex =
        new("^[a-z ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (ProductNameRegex.IsMatch(Value))
        {
            return;
        }

        var message = $"{Value} is not a valid product name";
        throw new ValidationException(message, new []
        {
            new ValidationFailure(nameof(ProductName), message)
        });
    }
}
