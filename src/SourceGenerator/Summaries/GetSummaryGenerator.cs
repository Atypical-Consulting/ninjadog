using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Summaries;

[Generator]
public sealed class GetSummaryGenerator : IIncrementalGenerator
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

            StringVariations sv = new(type.Name);
            string className = $"Get{sv.Pascal}Summary";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Summaries" : null;
        StringVariations sv = new(type.Name);

        string className = $"Get{sv.Pascal}Summary";
        string getModelEndpoint = $"Get{sv.Pascal}Endpoint";
        string getModelResponse = $"Get{sv.Pascal}Response";
        string humanized = sv.Humanized;

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public class {className} : Summary<{getModelEndpoint}>
    {{
        public {className}()
        {{
            Summary = ""Returns a single {humanized} by id"";
            Description = ""Returns a single {humanized} by id"";
            Response<{getModelResponse}>(200, ""Successfully found and returned the {humanized}"");
            Response(404, ""The {humanized} does not exist in the system"");
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
