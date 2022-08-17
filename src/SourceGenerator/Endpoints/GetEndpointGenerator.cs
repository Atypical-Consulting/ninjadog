using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Endpoints;

[Generator]
public sealed class GetEndpointGenerator : IIncrementalGenerator
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
            var className = $"Get{sv.Pascal}Endpoint";

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
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{Utilities.WriteFileScopedNamespace(ns)}

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

        return Utilities.DefaultCodeLayout(code);
    }
}
