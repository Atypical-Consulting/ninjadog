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
        StringVariations sv = new(type.Name);

        string endpointAddress = sv.Dashed;
        string className = $"GetAll{sv.PascalPlural}Endpoint";
        string response = $"GetAll{sv.PascalPlural}Response";
        string interfaceService = $"I{sv.Pascal}Service";
        string fieldService = $"_{sv.Camel}Service";
        string varService = $"{sv.Camel}Service";
        string varModels = sv.CamelPlural;
        string varModelsResponse = $"{sv.CamelPlural}Response";
        string methodToModelsResponse = $"To{sv.PascalPlural}Response()";

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{(ns is null ? null : $@"namespace {ns}
{{")}
    [HttpGet(""{endpointAddress}""), AllowAnonymous]
    public partial class {className} : EndpointWithoutRequest<{response}>
    {{
        private readonly {interfaceService} {fieldService};

        public {className}({interfaceService} {varService})
        {{
            {fieldService} = {varService};
        }}

        public override async Task HandleAsync(CancellationToken ct)
        {{
            var {varModels} = await {fieldService}.GetAllAsync();
            var {varModelsResponse} = {varModels}.{methodToModelsResponse};
            await SendOkAsync({varModelsResponse}, ct);
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
