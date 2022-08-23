using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Summaries;

[Generator]
public sealed class DeleteSummaryGenerator : NinjadogBaseGenerator
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
            var className = $"Delete{st.Model}Summary";

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
using {rootNs}.Endpoints;
using FastEndpoints;

{WriteFileScopedNamespace(ns)}

public partial class {_.ClassDeleteModelSummary} : Summary<{_.ClassDeleteModelEndpoint}>
{{
    public {_.ClassDeleteModelSummary}()
    {{
        Summary = ""Delete a {_.ModelHumanized} in the system"";
        Description = ""Delete a {_.ModelHumanized} in the system"";
        Response(204, ""The {_.ModelHumanized} was deleted successfully"");
        Response(404, ""The {_.ModelHumanized} was not found in the system"");
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
