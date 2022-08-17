using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Repositories;

[Generator]
public sealed class RepositoryInterfaceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ITypeSymbol>> enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(Utilities.CouldBeEnumerationAsync, Utilities.GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;

        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> enumerations)
    {
        if (enumerations.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in enumerations)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Repositories";

            StringVariations sv = new(type.Name);
            var className = $"I{sv.Pascal}Repository";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Repositories" : null;

        StringTokens _ = new(type.Name);

        string code = @$"
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
