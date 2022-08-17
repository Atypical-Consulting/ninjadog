using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Responses;

[Generator]
public sealed class GetAllResponseGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Responses";

            StringVariations sv = new(type.Name);
            var className = $"GetAll{sv.PascalPlural}Response";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Contracts.Responses" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial class {_.ClassGetAllModelsResponse}
    {{
        public IEnumerable<{_.ClassModelResponse}> {_.Models} {{ get; init; }} = Enumerable.Empty<{_.ClassModelResponse}>();
    }}
{(ns is null ? null : @"}
")}";

        return Utilities.DefaultCodeLayout(code);
    }
}
