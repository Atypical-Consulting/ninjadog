using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Endpoints;

[Generator]
public sealed class CreateEndpointGenerator : NinjadogBaseGenerator
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
            var className = $"Create{st.Model}Endpoint";

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

[HttpPost(""{_.ModelEndpoint}""), AllowAnonymous]
public partial class {_.ClassCreateModelEndpoint} : Endpoint<{_.ClassCreateModelRequest}, {_.ClassModelResponse}>
{{
    private readonly {_.InterfaceModelService} {_.FieldModelService};

    public {_.ClassCreateModelEndpoint}({_.InterfaceModelService} {_.VarModelService})
    {{
        {_.FieldModelService} = {_.VarModelService};
    }}

    public override async Task HandleAsync({_.ClassCreateModelRequest} req, CancellationToken ct)
    {{
        var {_.VarModel} = req.{_.MethodToModel}();

        await {_.FieldModelService}.CreateAsync({_.VarModel});

        var {_.VarModelResponse} = {_.VarModel}.{_.MethodToModelResponse}();
        await SendCreatedAtAsync<{_.ClassGetModelEndpoint}>(
            new {{ Id = {_.VarModel}.Id.Value }}, {_.VarModelResponse}, generateAbsoluteUrl: true, cancellation: ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
