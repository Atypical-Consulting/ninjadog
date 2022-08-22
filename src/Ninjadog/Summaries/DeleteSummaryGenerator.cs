using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog.Summaries;

[Generator]
public sealed class DeleteSummaryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modelTypes = Utilities.CollectNinjadogModelTypes(context);

        context.RegisterSourceOutput(modelTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Summaries";

            StringTokens st = new(type.Name);
            var className = $"Delete{st.Model}Summary";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Summaries" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Endpoints;
using FastEndpoints;

{Utilities.WriteFileScopedNamespace(ns)}

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

        return Utilities.DefaultCodeLayout(code);
    }
}
