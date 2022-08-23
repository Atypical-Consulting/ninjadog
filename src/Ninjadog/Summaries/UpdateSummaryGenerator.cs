using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Summaries;

[Generator]
public sealed class UpdateSummaryGenerator : NinjadogBaseGenerator
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
            var className = $"Update{st.Model}Summary";

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

public partial class {_.ClassUpdateModelSummary} : Summary<{_.ClassUpdateModelEndpoint}>
{{
    public {_.ClassUpdateModelSummary}()
    {{
        Summary = ""Updates an existing {_.ModelHumanized} in the system"";
        Description = ""Updates an existing {_.ModelHumanized} in the system"";
        Response<{_.ClassModelResponse}>(201, ""{_.ModelHumanized} was successfully updated"");
        Response<ValidationFailureResponse>(400, ""The request did not pass validation checks"");
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
