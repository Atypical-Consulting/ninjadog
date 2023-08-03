namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class GetRequestGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"Get{st.Model}Request",
            GenerateCode,
            "Contracts.Requests");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;

        var code = $$"""
            
            {{WriteFileScopedNamespace(ns)}}
            
            /// <summary>
            ///     Request to get a {{st.Model}}.
            /// </summary>
            public partial class {{st.ClassGetModelRequest}}
            {
                public Guid Id { get; init; }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
