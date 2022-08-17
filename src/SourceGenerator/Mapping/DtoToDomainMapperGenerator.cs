using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class DtoToDomainMapperGenerator : IIncrementalGenerator
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

        const string className = "DtoToDomainMapperGenerator";

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

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Domain;
using {rootNs}.Domain.Common;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public static class DtoToDomainMapper
    {{
        public static Customer ToCustomer(this CustomerDto customerDto)
        {{
            return new Customer
            {{
                Id = CustomerId.From(Guid.Parse(customerDto.Id)),
                Email = EmailAddress.From(customerDto.Email),
                Username = Username.From(customerDto.Username),
                FullName = FullName.From(customerDto.FullName),
                DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(customerDto.DateOfBirth))
            }};
        }}
    }}
{(ns is null ? null : @"}
")}";

        return Utilities.DefaultCodeLayout(code);
    }
}
