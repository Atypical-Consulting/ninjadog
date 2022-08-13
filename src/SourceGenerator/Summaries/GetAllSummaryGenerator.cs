using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Summaries;

[Generator]
public sealed class GetAllSummaryGenerator : IIncrementalGenerator
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
            string className = $"GetAll{sv.PascalPlural}Summary";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Summaries" : null;
        StringVariations sv = new(type.Name);

        string className = $"GetAll{sv.PascalPlural}Summary";
        string getAllEndpoint = $"GetAll{sv.PascalPlural}Endpoint";
        string getAllResponse = $"GetAll{sv.PascalPlural}Response";
        string humanized = sv.HumanizedPlural;

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public class {className} : Summary<{getAllEndpoint}>
    {{
        public {className}()
        {{
            Summary = ""Returns all the {humanized} in the system"";
            Description = ""Returns all the {humanized} in the system"";
            Response<{getAllResponse}>(200, ""All {humanized} in the system are returned"");
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
