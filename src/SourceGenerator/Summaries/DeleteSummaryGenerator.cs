using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Summaries;

[Generator]
public sealed class DeleteSummaryGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Summaries";

            context.AddSource($"{typeNamespace}.{type.Name}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Summaries" : null;
        StringVariations sv = new(type.Name);

        string className = $"Delete{sv.Pascal}Summary";
        string deleteEndpoint = $"Delete{sv.Pascal}Endpoint";
        string humanized = sv.Humanized;

        return StringConstants.FileHeader + @$"

using {rootNs}.Endpoints;
using FastEndpoints;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public class {className} : Summary<{deleteEndpoint}>
    {{
        public {className}()
        {{
            Summary = ""Delete a {humanized} in the system"";
            Description = ""Delete a {humanized} in the system"";
            Response(204, ""The {humanized} was deleted successfully"");
            Response(404, ""The {humanized} was not found in the system"");
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
