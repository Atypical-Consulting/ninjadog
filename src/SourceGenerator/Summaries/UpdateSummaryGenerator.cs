using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Summaries;

[Generator]
public sealed class UpdateSummaryGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Summaries";

            StringVariations sv = new(type.Name);
            string className = $"Update{sv.Pascal}Summary";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Summaries" : null;
        StringVariations sv = new(type.Name);

        string className = $"Update{sv.Pascal}Summary";
        string updateModelEndpoint = $"Update{sv.Pascal}Endpoint";
        string modelResponse = $"{sv.Pascal}Response";
        string humanized = sv.Humanized;

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using FastEndpoints;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public class {className} : Summary<{updateModelEndpoint}>
    {{
        public {className}()
        {{
            Summary = ""Updates an existing {humanized} in the system"";
            Description = ""Updates an existing {humanized} in the system"";
            Response<{modelResponse}>(201, ""{humanized} was successfully updated"");
            Response<ValidationFailureResponse>(400, ""The request did not pass validation checks"");
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
