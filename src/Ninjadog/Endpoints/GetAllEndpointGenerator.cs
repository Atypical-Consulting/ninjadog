namespace Ninjadog.Endpoints;

[Generator]
public sealed class GetAllEndpointGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"GetAll{st.Models}Endpoint",
            GenerateCode,
            "Endpoints");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{WriteFileScopedNamespace(ns)}

[HttpGet(""{st.ModelEndpoint}""), AllowAnonymous]
public partial class {st.ClassGetAllModelsEndpoint} : EndpointWithoutRequest<{st.ClassGetAllModelsResponse}>
{{
    private readonly {st.InterfaceModelService} {st.FieldModelService};

    public {st.ClassGetAllModelsEndpoint}({st.InterfaceModelService} {st.VarModelService})
    {{
        {st.FieldModelService} = {st.VarModelService};
    }}

    public override async Task HandleAsync(CancellationToken ct)
    {{
        var {st.VarModels} = await {st.FieldModelService}.GetAllAsync();
        var {st.VarModelsResponse} = {st.VarModels}.{st.MethodToModelsResponse}();
        await SendOkAsync({st.VarModelsResponse}, ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
