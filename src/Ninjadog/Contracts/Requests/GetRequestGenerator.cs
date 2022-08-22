using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class GetRequestGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Requests";

            StringTokens st = new(type.Name);
            var className = $"Get{st.Model}Request";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Requests" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassGetModelRequest}
{{
    public Guid Id {{ get; init; }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
