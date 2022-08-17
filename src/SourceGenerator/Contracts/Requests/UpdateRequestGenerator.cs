using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Requests;

[Generator]
public sealed class UpdateRequestGenerator : IIncrementalGenerator
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
            var className = $"Update{sv.Pascal}Request";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Contracts.Requests" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial class {_.ClassUpdateModelRequest}
    {{
        // TODO: Generate properties

        // public Guid Id {{ get; init; }}

        // public string Username {{ get; init; }} = default!;

        // public string FullName {{ get; init; }} = default!;

        // public string Email {{ get; init; }} = default!;

        // public DateTime DateOfBirth {{ get; init; }}
    }}
{(ns is null ? null : @"}
")}";

        return Utilities.DefaultCodeLayout(code);
    }
}
