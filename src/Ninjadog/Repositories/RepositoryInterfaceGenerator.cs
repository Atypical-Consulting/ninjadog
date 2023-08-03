namespace Ninjadog.Repositories;

[Generator]
public sealed class RepositoryInterfaceGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"I{st.Model}Repository",
            GenerateCode,
            "Repositories");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""
            
            using {{rootNs}}.Contracts.Data;
            using {{rootNs}}.Database;
            using Dapper;
            
            {{WriteFileScopedNamespace(ns)}}
            
            public partial interface {{st.InterfaceModelRepository}}
            {
                Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}});
            
                Task<{{st.ClassModelDto}}?> GetAsync(Guid id);
            
                Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync();
            
                Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}});
            
                Task<bool> DeleteAsync(Guid id);
            }
            """;

        return DefaultCodeLayout(code);
    }
}
