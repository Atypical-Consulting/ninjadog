using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Requests;

[Generator]
public sealed class CreateRequestGenerator : IIncrementalGenerator
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

        foreach (var type in enumerations)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Requests";

            StringVariations sv = new(type.Name);
            var className = $"Create{sv.Pascal}Request";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Requests" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassCreateModelRequest}
{{
    // TODO: Generate properties

    // public string Username {{ get; init; }} = default!;

    // public string FullName {{ get; init; }} = default!;

    // public string Email {{ get; init; }} = default!;

    // public DateTime DateOfBirth {{ get; init; }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
