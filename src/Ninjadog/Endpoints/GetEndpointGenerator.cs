namespace Ninjadog.Endpoints;

[Generator]
public sealed class GetEndpointGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Get{st.Model}Endpoint",
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

public partial class {st.ClassGetModelEndpoint}
    : Endpoint<{st.ClassGetModelRequest}, {st.ClassModelResponse}>
{{
    public {st.InterfaceModelService} {st.PropertyModelService} {{ get; private set; }} = null!;

    public override void Configure()
    {{
        Get(""{st.ModelEndpoint}/{{id:guid}}"");
        AllowAnonymous();
    }}

    public override async Task HandleAsync({st.ClassGetModelRequest} req, CancellationToken ct)
    {{
        var {st.VarModel} = await {st.PropertyModelService}.GetAsync(req.Id);

        if ({st.VarModel} is null)
        {{
            await SendNotFoundAsync(ct);
            return;
        }}

        var {st.VarModelResponse} = {st.VarModel}.{st.MethodToModelResponse}();
        await SendOkAsync({st.VarModelResponse}, ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
