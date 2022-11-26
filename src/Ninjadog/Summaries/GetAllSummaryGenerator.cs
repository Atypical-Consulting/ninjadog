namespace Ninjadog.Summaries;

[Generator]
public sealed class GetAllSummaryGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"GetAll{st.Models}Summary",
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

public partial class {st.ClassGetAllModelsSummary} : Summary<{st.ClassGetAllModelsEndpoint}>
{{
    public {st.ClassGetAllModelsSummary}()
    {{
        Summary = ""Returns all the {st.ModelsHumanized} in the system"";
        Description = ""Returns all the {st.ModelsHumanized} in the system"";
        Response<{st.ClassGetAllModelsResponse}>(200, ""All {st.ModelsHumanized} in the system are returned"");
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
