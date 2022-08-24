namespace Ninjadog.Endpoints;

[Generator]
public sealed class CreateEndpointGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Create{st.Model}Endpoint",
            GenerateCode,
            "Endpoints");

    private static string GenerateCode(TypeContext typeContext)
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

[HttpPost(""{st.ModelEndpoint}""), AllowAnonymous]
public partial class {st.ClassCreateModelEndpoint} : Endpoint<{st.ClassCreateModelRequest}, {st.ClassModelResponse}>
{{
    private readonly {st.InterfaceModelService} {st.FieldModelService};

    public {st.ClassCreateModelEndpoint}({st.InterfaceModelService} {st.VarModelService})
    {{
        {st.FieldModelService} = {st.VarModelService};
    }}

    public override async Task HandleAsync({st.ClassCreateModelRequest} req, CancellationToken ct)
    {{
        var {st.VarModel} = req.{st.MethodToModel}();

        await {st.FieldModelService}.CreateAsync({st.VarModel});

        var {st.VarModelResponse} = {st.VarModel}.{st.MethodToModelResponse}();
        await SendCreatedAtAsync<{st.ClassGetModelEndpoint}>(
            new {{ Id = {st.VarModel}.Id.Value }}, {st.VarModelResponse}, generateAbsoluteUrl: true, cancellation: ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
