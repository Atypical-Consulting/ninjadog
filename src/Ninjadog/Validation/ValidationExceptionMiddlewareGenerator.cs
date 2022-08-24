namespace Ninjadog.Validation;

[Generator]
public sealed class ValidationExceptionMiddlewareGenerator : NinjadogBaseGenerator
{
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            "ValidationExceptionMiddlewareGenerator",
            GenerateCode,
            "Validation");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var (_, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

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
