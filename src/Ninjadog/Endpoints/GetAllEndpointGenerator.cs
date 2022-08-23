using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Endpoints;

[Generator]
public sealed class GetAllEndpointGenerator : NinjadogBaseGenerator
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
            var className = $"GetAll{st.Models}Endpoint";

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
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{WriteFileScopedNamespace(ns)}

[HttpGet(""{_.ModelEndpoint}""), AllowAnonymous]
public partial class {_.ClassGetAllModelsEndpoint} : EndpointWithoutRequest<{_.ClassGetAllModelsResponse}>
{{
    private readonly {_.InterfaceModelService} {_.FieldModelService};

    public {_.ClassGetAllModelsEndpoint}({_.InterfaceModelService} {_.VarModelService})
    {{
        {_.FieldModelService} = {_.VarModelService};
    }}

    public override async Task HandleAsync(CancellationToken ct)
    {{
        var {_.VarModels} = await {_.FieldModelService}.GetAllAsync();
        var {_.VarModelsResponse} = {_.VarModels}.{_.MethodToModelsResponse}();
        await SendOkAsync({_.VarModelsResponse}, ct);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
