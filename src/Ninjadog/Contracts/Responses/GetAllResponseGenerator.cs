using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Contracts.Responses;

[Generator]
public sealed class GetAllResponseGenerator : NinjadogBaseGenerator
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
            var className = $"GetAll{st.Models}Response";

            context.AddSource(
                $"{GetRootNamespace(type)}.Contracts.Responses.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Responses" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
{WriteFileScopedNamespace(ns)}

public partial class {_.ClassGetAllModelsResponse}
{{
    public IEnumerable<{_.ClassModelResponse}> {_.Models} {{ get; init; }} = Enumerable.Empty<{_.ClassModelResponse}>();
}}";

        return DefaultCodeLayout(code);
    }
}
