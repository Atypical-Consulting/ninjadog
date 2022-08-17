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

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassGetAllModelsSummary} : Summary<{_.ClassGetAllModelsEndpoint}>
{{
    public {_.ClassGetAllModelsSummary}()
    {{
        Summary = ""Returns all the {_.ModelHumanized} in the system"";
        Description = ""Returns all the {_.ModelHumanized} in the system"";
        Response<{_.ClassGetAllModelsResponse}>(200, ""All {_.ModelHumanized} in the system are returned"");
    }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
