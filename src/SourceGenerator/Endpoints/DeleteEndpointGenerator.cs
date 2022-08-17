using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Endpoints;

[Generator]
public sealed class DeleteEndpointGenerator : IIncrementalGenerator
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
            var className = $"Delete{sv.Pascal}Endpoint";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Endpoints" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{(ns is null ? null : $@"namespace {ns}
{{")}
    [HttpDelete(""{_.EndpointModels}/{{id:guid}}""), AllowAnonymous]
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
    }}
{(ns is null ? null : @"}
")}";

        return Utilities.DefaultCodeLayout(code);
    }
}
