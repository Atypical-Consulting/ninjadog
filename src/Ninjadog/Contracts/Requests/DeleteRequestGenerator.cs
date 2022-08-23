using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class DeleteRequestGenerator : NinjadogBaseGenerator
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
            var className = $"Delete{st.Model}Request";

            context.AddSource(
                $"{GetRootNamespace(type)}.Contracts.Requests.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Requests" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
{WriteFileScopedNamespace(ns)}

public partial class {_.ClassDeleteModelRequest}
{{
    public Guid Id {{ get; init; }}
}}";

        return DefaultCodeLayout(code);
    }
}
