using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Validation;

[Generator]
public sealed class ValidationExceptionMiddlewareGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        var type = models[0];
        const string className = "ValidationExceptionMiddlewareGenerator";

        context.AddSource(
            $"{GetRootNamespace(type)}.Validation.{className}.g.cs",
            GenerateCode(type));
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Validation" : null;

        var code = @$"
using {rootNs}.Contracts.Responses;
using FluentValidation;

{WriteFileScopedNamespace(ns)}

public class ValidationExceptionMiddleware
{{
    private readonly RequestDelegate _request;

    public ValidationExceptionMiddleware(RequestDelegate request)
    {{
        _request = request;
    }}

    public async Task InvokeAsync(HttpContext context)
    {{
        try
        {{
            await _request(context);
        }}
        catch (ValidationException exception)
        {{
            context.Response.StatusCode = 400;
            var messages = exception.Errors.Select(x => x.ErrorMessage).ToList();
            var validationFailureResponse = new ValidationFailureResponse
            {{
                Errors = messages
            }};
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }}
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
