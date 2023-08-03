namespace Ninjadog.Summaries;

[Generator]
public sealed class CreateSummaryGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"Create{st.Model}Summary",
            GenerateCode,
            "Summaries");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""
            
            using {{rootNs}}.Contracts.Responses;
            using {{rootNs}}.Endpoints;
            using FastEndpoints;
            
            {{WriteFileScopedNamespace(ns)}}
            
            public partial class {{st.ClassCreateModelSummary}} : Summary<{{st.ClassCreateModelEndpoint}}>
            {
                public {{st.ClassCreateModelSummary}}()
                {
                    Summary = "Creates a new {{st.ModelHumanized}} in the system";
                    Description = "Creates a new {{st.ModelHumanized}} in the system";
                    Response<{{st.ClassModelResponse}}>(201, "{{st.ModelHumanized}} was successfully created");
                    Response<ErrorResponse>(400, "The request did not pass validation checks");
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
