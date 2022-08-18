using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Ninjadog.Summaries;

[Generator]
public sealed class GetSummaryGenerator : IIncrementalGenerator
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

            StringVariations sv = new(type.Name);
            var className = $"Get{sv.Pascal}Summary";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Summaries" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{Utilities.WriteFileScopedNamespace(ns)}

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

        return Utilities.DefaultCodeLayout(code);
    }
}
