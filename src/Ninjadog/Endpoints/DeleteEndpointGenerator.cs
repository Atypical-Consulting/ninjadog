namespace Ninjadog.Endpoints;

[Generator]
public sealed class DeleteEndpointGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Delete{st.Model}Endpoint",
            GenerateCode,
            "Endpoints");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{WriteFileScopedNamespace(ns)}

[HttpDelete(""{st.ModelEndpoint}/{{id:guid}}""), AllowAnonymous]
public partial class {st.ClassDeleteModelEndpoint} : Endpoint<{st.ClassDeleteModelRequest}>
{{
    private readonly {st.InterfaceModelService} {st.FieldModelService};

    public {st.ClassDeleteModelEndpoint}({st.InterfaceModelService} {st.VarModelService})
    {{
        {st.FieldModelService} = {st.VarModelService};
    }}

    public override async Task HandleAsync({st.ClassDeleteModelRequest} req, CancellationToken ct)
    {{
        var deleted = await {st.FieldModelService}.DeleteAsync(req.Id);
        if (!deleted)
        {{
            await SendNotFoundAsync(ct);
            return;
        }}

        await SendNoContentAsync(ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
