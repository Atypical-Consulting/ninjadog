using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Summaries;

[Generator]
public sealed class CreateSummaryGenerator : IIncrementalGenerator
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
            string className = $"Create{sv.Pascal}Summary";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Summaries" : null;
        StringVariations sv = new(type.Name);

        string className = $"Create{sv.Pascal}Summary";
        string createEndpoint = $"Create{sv.Pascal}Endpoint";
        string humanized = sv.Humanized;
        string modelResponse = $"{sv.Pascal}Response";

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Responses;
using {rootNs}.Endpoints;
using DemoLibrary.Contracts.Responses;
using FastEndpoints;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public class {className} : Summary<{createEndpoint}>
    {{
        public {className}()
        {{
            Summary = ""Creates a new {humanized} in the system"";
            Description = ""Creates a new {humanized} in the system"";
            Response<{modelResponse}>(201, ""{humanized} was successfully created"");
            Response<ValidationFailureResponse>(400, ""The request did not pass validation checks"");
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
