using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class DomainToDtoMapperGenerator : IIncrementalGenerator
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

        var type = enumerations[0];
        var code = GenerateCode(type);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Mapping";

        const string className = "DomainToDtoMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Mapping" : null;
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Data;
using {rootNs}.Domain;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public static class DomainToDtoMapper
    {{
        public static CustomerDto ToCustomerDto(this Customer customer)
        {{
            return new CustomerDto
            {{
                Id = customer.Id.Value.ToString(),
                Email = customer.Email.Value,
                Username = customer.Username.Value,
                FullName = customer.FullName.Value,
                DateOfBirth = customer.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
            }};
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
