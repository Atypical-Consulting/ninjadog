namespace Ninjadog.Contracts.Responses;

[Generator]
public sealed class GetAllResponseGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"GetAll{st.Models}Response",
            GenerateCode,
            "Contracts.Responses");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;

        var code = $$"""
            
            {{WriteFileScopedNamespace(ns)}}
            
            public partial class {{st.ClassGetAllModelsResponse}}
            {
                public IEnumerable<{{st.ClassModelResponse}}> {{st.Models}} { get; init; } = Enumerable.Empty<{{st.ClassModelResponse}}>();
            }
            """;

        return DefaultCodeLayout(code);
    }
}
