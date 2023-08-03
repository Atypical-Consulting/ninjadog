// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace DemoApi.Domain.Common;

public class ProductDescription : ValueOf<string, ProductDescription>
{
    private const int MaxLength = 1000;

    protected override void Validate()
    {
        if (Value.Length <= MaxLength)
        {
            return;
        }

        var message = $"{Value} is not a valid product description";
        throw new ValidationException(message, new []
        {
            new ValidationFailure(nameof(ProductDescription), message)
        });
    }
}
