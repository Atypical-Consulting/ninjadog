namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class DeleteRequestGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Delete{st.Model}Request",
            GenerateCode,
            "Contracts.Requests");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;

        var code = $$"""
            
            {{WriteFileScopedNamespace(ns)}}
            
            /// <summary>
            ///     Request to delete a {{st.Model}}.
            /// </summary>
            public partial class {{st.ClassDeleteModelRequest}}
            {
                public Guid Id { get; init; }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
