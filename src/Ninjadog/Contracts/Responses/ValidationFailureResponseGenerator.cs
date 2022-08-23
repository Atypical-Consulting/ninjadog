using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Contracts.Responses;

[Generator]
public sealed class ValidationFailureResponseGenerator : NinjadogBaseGenerator
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
        const string className = "ValidationFailureResponse";

        context.AddSource(
            $"{GetRootNamespace(type)}.Contracts.Responses.{className}.g.cs",
            GenerateCode(type));
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Responses" : null;

        var code = @$"
{WriteFileScopedNamespace(ns)}

public class ValidationFailureResponse
{{
    public List<string> Errors {{ get; init; }} = new();
}}";

        return DefaultCodeLayout(code);
    }
}
