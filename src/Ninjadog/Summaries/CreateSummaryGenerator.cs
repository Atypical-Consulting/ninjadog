using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Ninjadog.Summaries;

[Generator]
public sealed class CreateSummaryGenerator : IIncrementalGenerator
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
            var className = $"Create{sv.Pascal}Summary";

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

public partial class {_.ClassCreateModelSummary} : Summary<{_.ClassCreateModelEndpoint}>
{{
    public {_.ClassCreateModelSummary}()
    {{
        Summary = ""Creates a new {_.ModelHumanized} in the system"";
        Description = ""Creates a new {_.ModelHumanized} in the system"";
        Response<{_.ClassModelResponse}>(201, ""{_.ModelHumanized} was successfully created"");
        Response<ValidationFailureResponse>(400, ""The request did not pass validation checks"");
    }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
