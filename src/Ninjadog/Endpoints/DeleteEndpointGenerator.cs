using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog.Endpoints;

[Generator]
public sealed class DeleteEndpointGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Endpoints";

            StringTokens st = new(type.Name);
            var className = $"Delete{st.Model}Endpoint";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Endpoints" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{Utilities.WriteFileScopedNamespace(ns)}

[HttpDelete(""{_.ModelEndpoint}/{{id:guid}}""), AllowAnonymous]
public partial class {_.ClassDeleteModelEndpoint} : Endpoint<{_.ClassDeleteModelRequest}>
{{
    private readonly {_.InterfaceModelService} {_.FieldModelService};

    public {_.ClassDeleteModelEndpoint}({_.InterfaceModelService} {_.VarModelService})
    {{
        {_.FieldModelService} = {_.VarModelService};
    }}

    public override async Task HandleAsync({_.ClassDeleteModelRequest} req, CancellationToken ct)
    {{
        var deleted = await {_.FieldModelService}.DeleteAsync(req.Id);
        if (!deleted)
        {{
            await SendNotFoundAsync(ct);
            return;
        }}

        await SendNoContentAsync(ct);
    }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
