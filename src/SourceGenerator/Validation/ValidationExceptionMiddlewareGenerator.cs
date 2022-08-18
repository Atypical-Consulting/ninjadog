using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Validation;

[Generator]
public sealed class ValidationExceptionMiddlewareGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modelTypes = Utilities.CollectNinjadogModelTypes(context);

        context.RegisterSourceOutput(modelTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        var type = models[0];
        var code = GenerateCode(type);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Validation";

        const string className = "ValidationExceptionMiddlewareGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Validation" : null;

        var code = @$"
using {rootNs}.Contracts.Responses;
using FluentValidation;

{Utilities.WriteFileScopedNamespace(ns)}

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

        return Utilities.DefaultCodeLayout(code);
    }
}
