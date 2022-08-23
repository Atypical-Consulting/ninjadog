using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog.Services;

[Generator]
public sealed class ServiceInterfaceGenerator : NinjadogBaseGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Services";

            StringTokens st = new(type.Name);
            var className = $"I{st.Model}Service";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Services" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Domain;

{Utilities.WriteFileScopedNamespace(ns)}

public partial interface {_.InterfaceModelService}
{{
    Task<bool> CreateAsync({_.Model} {_.VarModel});

    Task<{_.Model}?> GetAsync(Guid id);

    Task<IEnumerable<{_.Model}>> GetAllAsync();

    Task<bool> UpdateAsync({_.Model} {_.VarModel});

    Task<bool> DeleteAsync(Guid id);
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
