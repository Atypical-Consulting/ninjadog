using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Responses;

[Generator]
public sealed class ValidationFailureResponseGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ITypeSymbol>> enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(Utilities.CouldBeEnumerationAsync, Utilities.GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;

        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> enumerations)
    {
        if (enumerations.IsDefaultOrEmpty)
        {
            return;
        }

        var type = enumerations[0];
        var code = GenerateCode(type);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Responses";

        const string className = "ValidationFailureResponse";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Responses" : null;

        var code = @$"
{Utilities.WriteFileScopedNamespace(ns)}

public class ValidationFailureResponse
{{
    public List<string> Errors {{ get; init; }} = new();
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
