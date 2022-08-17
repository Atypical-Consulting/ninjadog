using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Responses;

[Generator]
public sealed class ResponseGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Responses";

            StringVariations sv = new(type.Name);
            var className = $"{sv.Pascal}Response";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Contracts.Responses" : null;
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial class {name}Response
    {{
        public Guid Id {{ get; init; }}

        public string Username {{ get; init; }} = default!;

        public string FullName {{ get; init; }} = default!;

        public string Email {{ get; init; }} = default!;

        public DateTime DateOfBirth {{ get; init; }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
