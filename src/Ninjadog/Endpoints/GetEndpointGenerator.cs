using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Endpoints;

[Generator]
public sealed class GetEndpointGenerator : NinjadogBaseGenerator
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
            StringTokens st = new(type.Name);
            var className = $"Get{st.Model}Endpoint";

            context.AddSource(
                $"{GetRootNamespace(type)}.Endpoints.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Endpoints" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{WriteFileScopedNamespace(ns)}

[HttpGet(""{_.ModelEndpoint}/{{id:guid}}""), AllowAnonymous]
public partial class {_.ClassGetModelEndpoint} : Endpoint<{_.ClassGetModelRequest}, {_.ClassModelResponse}>
{{
    private readonly {_.InterfaceModelService} {_.FieldModelService};

    public {_.ClassGetModelEndpoint}({_.InterfaceModelService} {_.VarModelService})
    {{
        {_.FieldModelService}= {_.VarModelService};
    }}

    public override async Task HandleAsync({_.ClassGetModelRequest} req, CancellationToken ct)
    {{
        var {_.VarModel} = await {_.FieldModelService}.GetAsync(req.Id);

        if ({_.VarModel} is null)
        {{
            await SendNotFoundAsync(ct);
            return;
        }}

        var {_.VarModelResponse} = {_.VarModel}.{_.MethodToModelResponse}();
        await SendOkAsync({_.VarModelResponse}, ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
