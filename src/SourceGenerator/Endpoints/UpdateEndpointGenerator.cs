using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Endpoints;

[Generator]
public sealed class UpdateEndpointGenerator : IIncrementalGenerator
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
            var typeNamespace = type.ContainingNamespace.IsGlobalNamespace
                ? null
                : $"{type.ContainingNamespace}.";

            context.AddSource($"{typeNamespace}{type.Name}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Endpoints" : null;
        StringVariations sv = new(type.Name);
        
        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Requests;
using {rootNs}.Contracts.Responses;
using {rootNs}.Mapping;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{(ns is null ? null : $@"namespace {ns}
{{")}
    [HttpPut(""customers/{{id:guid}}""), AllowAnonymous]
    public class Update{name}Endpoint : Endpoint<Update{name}Request, {name}Response>
    {{
        private readonly I{name}Service _{lower}Service;

        public Update{name}Endpoint(I{name}Service {lower}Service)
        {{
            _{lower}Service = {lower}Service;
        }}

        public override async Task HandleAsync(Update{name}Request req, CancellationToken ct)
        {{
            var existing{name} = await _{lower}Service.GetAsync(req.Id);

            if (existing{name} is null)
            {{
                await SendNotFoundAsync(ct);
                return;
            }}

            var {lower} = req.To{name}();
            await _{lower}Service.UpdateAsync({lower});

            var {lower}Response = {lower}.To{name}Response();
            await SendOkAsync({lower}Response, ct);
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}