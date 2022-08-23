using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Summaries;

[Generator]
public sealed class GetSummaryGenerator : NinjadogBaseGenerator
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
            var className = $"Get{st.Model}Summary";

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

public partial class {_.ClassGetModelSummary} : Summary<{_.ClassGetModelEndpoint}>
{{
    public {_.ClassGetModelSummary}()
    {{
        Summary = ""Returns a single {_.ModelHumanized} by id"";
        Description = ""Returns a single {_.ModelHumanized} by id"";
        Response<{_.ClassModelResponse}>(200, ""Successfully found and returned the {_.ModelHumanized}"");
        Response(404, ""The {_.ModelHumanized} does not exist in the system"");
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
