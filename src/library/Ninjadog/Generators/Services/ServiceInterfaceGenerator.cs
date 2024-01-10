namespace Ninjadog.Services;

[Generator]
public sealed class ServiceInterfaceGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"I{st.Model}Service",
            GenerateCode,
            "Services");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""
            
            using {{rootNs}}.Domain;
            
            {{WriteFileScopedNamespace(ns)}}
            
            public partial interface {{st.InterfaceModelService}}
            {
                Task<bool> CreateAsync({{st.Model}} {{st.VarModel}});
            
                Task<{{st.Model}}?> GetAsync(Guid id);
            
                Task<IEnumerable<{{st.Model}}>> GetAllAsync();
            
                Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}});
            
                Task<bool> DeleteAsync(Guid id);
            }
            """;

        return DefaultCodeLayout(code);
    }
}
