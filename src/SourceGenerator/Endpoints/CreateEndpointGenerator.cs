using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Endpoints;

[Generator]
public sealed class CreateEndpointGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ITypeSymbol>> enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(Utilities.CouldBeEnumerationAsync, Utilities.GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;

        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> enumerations)
    {
        if (enumerations.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in enumerations)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Endpoints";

            StringVariations sv = new(type.Name);
            var className = $"Create{sv.Pascal}Endpoint";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Endpoints" : null;

        StringTokens _ = new(type.Name);

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Requests;
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{(ns is null ? null : $@"namespace {ns}
{{")}
    [HttpPost(""{_.EndpointModels}""), AllowAnonymous]
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
    }}
{(ns is null ? null : @"}
")}";
    }
}
