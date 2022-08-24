namespace Ninjadog.Endpoints;

[Generator]
public sealed class UpdateEndpointGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Update{st.Model}Endpoint",
            GenerateCode,
            "Endpoints");

    private string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{WriteFileScopedNamespace(ns)}

[HttpPut(""{st.ModelEndpoint}/{{id:guid}}""), AllowAnonymous]
public partial class {st.ClassUpdateModelEndpoint} : Endpoint<{st.ClassUpdateModelRequest}, {st.ClassModelResponse}>
{{
    private readonly {st.InterfaceModelService} {st.FieldModelService};

    public {st.ClassUpdateModelEndpoint}({st.InterfaceModelService} {st.VarModelService})
    {{
        {st.FieldModelService} = {st.VarModelService};
    }}

    public override async Task HandleAsync({st.ClassUpdateModelRequest} req, CancellationToken ct)
    {{
        var {st.VarExistingModel} = await {st.FieldModelService}.GetAsync(req.Id);

        if ({st.VarExistingModel} is null)
        {{
            await SendNotFoundAsync(ct);
            return;
        }}

        var {st.VarModel} = req.{st.MethodToModel}();
        await {st.FieldModelService}.UpdateAsync({st.VarModel});

        var {st.VarModelResponse} = {st.VarModel}.{st.MethodToModelResponse}();
        await SendOkAsync({st.VarModelResponse}, ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
