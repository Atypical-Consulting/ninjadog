using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog.Repositories;

[Generator]
public sealed class RepositoryInterfaceGenerator : NinjadogBaseGenerator
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
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Repositories";

            StringTokens st = new(type.Name);
            var className = $"I{st.Model}Repository";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Repositories" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Database;
using Dapper;

{Utilities.WriteFileScopedNamespace(ns)}

public partial interface {_.InterfaceModelRepository}
{{
    Task<bool> CreateAsync({_.ClassModelDto} {_.VarModel});

    Task<{_.ClassModelDto}?> GetAsync(Guid id);

    Task<IEnumerable<{_.ClassModelDto}>> GetAllAsync();

    Task<bool> UpdateAsync({_.ClassModelDto} {_.VarModel});

    Task<bool> DeleteAsync(Guid id);
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
