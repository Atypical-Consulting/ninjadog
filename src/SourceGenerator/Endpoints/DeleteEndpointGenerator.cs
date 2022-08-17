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
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Requests;
using {rootNs}.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

{(ns is null ? null : $@"namespace {ns}
{{")}
    [HttpDelete(""customers/{{id:guid}}""), AllowAnonymous]
    public partial class Delete{name}Endpoint : Endpoint<Delete{name}Request>
    {{
        private readonly I{name}Service _{lower}Service;

        public Delete{name}Endpoint(I{name}Service {lower}Service)
        {{
            _{lower}Service = {lower}Service;
        }}

        public override async Task HandleAsync(Delete{name}Request req, CancellationToken ct)
        {{
            var deleted = await _{lower}Service.DeleteAsync(req.Id);
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
    }
}
