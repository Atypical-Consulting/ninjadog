namespace Ninjadog.Contracts.Responses;

[Generator]
public sealed class ValidationFailureResponseGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            "ValidationFailureResponse",
            GenerateCode,
            "Contracts.Responses");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;

        var code = @$"
{WriteFileScopedNamespace(ns)}

public class ValidationFailureResponse
{{
    public List<string> Errors {{ get; init; }} = new();
}}";

        return DefaultCodeLayout(code);
    }
}
