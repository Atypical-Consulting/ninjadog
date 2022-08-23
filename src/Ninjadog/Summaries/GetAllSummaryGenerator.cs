using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Summaries;

[Generator]
public sealed class GetAllSummaryGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            StringTokens st = new(type.Name);
            var className = $"GetAll{st.Models}Summary";

            context.AddSource(
                $"{GetRootNamespace(type)}.Summaries.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Summaries" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{WriteFileScopedNamespace(ns)}

public partial class {_.ClassGetAllModelsSummary} : Summary<{_.ClassGetAllModelsEndpoint}>
{{
    public {_.ClassGetAllModelsSummary}()
    {{
        Summary = ""Returns all the {_.ModelsHumanized} in the system"";
        Description = ""Returns all the {_.ModelsHumanized} in the system"";
        Response<{_.ClassGetAllModelsResponse}>(200, ""All {_.ModelsHumanized} in the system are returned"");
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
