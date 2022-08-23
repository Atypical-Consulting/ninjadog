using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Endpoints;

[Generator]
public sealed class UpdateEndpointGenerator : NinjadogBaseGenerator
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
            var className = $"Update{st.Model}Endpoint";

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

[HttpPut(""{_.ModelEndpoint}/{{id:guid}}""), AllowAnonymous]
public partial class {_.ClassUpdateModelEndpoint} : Endpoint<{_.ClassUpdateModelRequest}, {_.ClassModelResponse}>
{{
    private readonly {_.InterfaceModelService} {_.FieldModelService};

    public {_.ClassUpdateModelEndpoint}({_.InterfaceModelService} {_.VarModelService})
    {{
        {_.FieldModelService} = {_.VarModelService};
    }}

    public override async Task HandleAsync({_.ClassUpdateModelRequest} req, CancellationToken ct)
    {{
        var {_.VarExistingModel} = await {_.FieldModelService}.GetAsync(req.Id);

        if ({_.VarExistingModel} is null)
        {{
            await SendNotFoundAsync(ct);
            return;
        }}

        var {_.VarModel} = req.{_.MethodToModel}();
        await {_.FieldModelService}.UpdateAsync({_.VarModel});

        var {_.VarModelResponse} = {_.VarModel}.{_.MethodToModelResponse}();
        await SendOkAsync({_.VarModelResponse}, ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
