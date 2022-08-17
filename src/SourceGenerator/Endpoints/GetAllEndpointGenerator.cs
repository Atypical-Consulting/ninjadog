using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Endpoints;

[Generator]
public sealed class GetAllEndpointGenerator : IIncrementalGenerator
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
            string code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Endpoints";

            StringVariations sv = new(type.Name);
            var className = $"GetAll{sv.PascalPlural}Endpoint";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Endpoints" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{Utilities.WriteFileScopedNamespace(ns)}

[HttpGet(""{_.EndpointModels}""), AllowAnonymous]
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

        return Utilities.DefaultCodeLayout(code);
    }
}
