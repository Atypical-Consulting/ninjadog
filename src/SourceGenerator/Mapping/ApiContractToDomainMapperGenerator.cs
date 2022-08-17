using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class ApiContractToDomainMapperGenerator : IIncrementalGenerator
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

        const string className = "ApiContractToDomainMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Domain;
using {rootNs}.Domain.Common;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public static class ApiContractToDomainMapper
    {{
        public static Customer ToCustomer(this CreateCustomerRequest request)
        {{
            return new Customer
            {{
                Id = CustomerId.From(Guid.NewGuid()),
                Email = EmailAddress.From(request.Email),
                Username = Username.From(request.Username),
                FullName = FullName.From(request.FullName),
                DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
            }};
        }}

        public static Customer ToCustomer(this UpdateCustomerRequest request)
        {{
            return new Customer
            {{
                Id = CustomerId.From(request.Id),
                Email = EmailAddress.From(request.Email),
                Username = Username.From(request.Username),
                FullName = FullName.From(request.FullName),
                DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
            }};
        }}
    }}
{(ns is null ? null : @"}
")}";

        return Utilities.DefaultCodeLayout(code);
    }
}
