namespace Ninjadog.Summaries;

[Generator]
public sealed class UpdateSummaryGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Update{st.Model}Summary",
            GenerateCode,
            "Summaries");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{WriteFileScopedNamespace(ns)}

public partial class {st.ClassUpdateModelSummary} : Summary<{st.ClassUpdateModelEndpoint}>
{{
    public {st.ClassUpdateModelSummary}()
    {{
        Summary = ""Updates an existing {st.ModelHumanized} in the system"";
        Description = ""Updates an existing {st.ModelHumanized} in the system"";
        Response<{st.ClassModelResponse}>(201, ""{st.ModelHumanized} was successfully updated"");
        Response<ErrorResponse>(400, ""The request did not pass validation checks"");
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
