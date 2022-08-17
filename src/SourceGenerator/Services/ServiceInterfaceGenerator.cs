using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Services;

[Generator]
public sealed class ServiceInterfaceGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Services";

            StringVariations sv = new(type.Name);
            var className = $"I{sv.Pascal}Service";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Services" : null;

        StringTokens _ = new(type.Name);

        return StringConstants.FileHeader + @$"

using System.Collections.Generic;
using {rootNs}.Database;
using Dapper;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial interface {_.InterfaceModelService}
    {{
        Task<bool> CreateAsync({_.ClassModel} {_.VarModel});

        Task<{_.ClassModel}?> GetAsync(Guid id);

        Task<IEnumerable<{_.ClassModel}>> GetAllAsync();

        Task<bool> UpdateAsync({_.ClassModel} {_.VarModel});

        Task<bool> DeleteAsync(Guid id);
    }}
{(ns is null ? null : @"}
")}";
    }
}
