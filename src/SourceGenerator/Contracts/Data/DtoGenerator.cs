using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Data;

[Generator]
public sealed class DtoGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Data";

            StringVariations sv = new(type.Name);
            var className = $"{sv.Pascal}Dto";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Data" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using System.Collections.Generic;
using {rootNs}.Database;
using Dapper;

{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassModelDto}
{{
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
