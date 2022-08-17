using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class DomainToApiContractMapperGenerator : IIncrementalGenerator
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

        const string className = "DomainToApiContractMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Domain;

{Utilities.WriteFileScopedNamespace(ns)}

public static class DomainToApiContractMapper
{{
    public static CustomerResponse ToCustomerResponse(this Customer customer)
    {{
        return new CustomerResponse
        {{
            Id = customer.Id.Value,
            Email = customer.Email.Value,
            Username = customer.Username.Value,
            FullName = customer.FullName.Value,
            DateOfBirth = customer.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        }};
    }}

    public static GetAllCustomersResponse ToCustomersResponse(this IEnumerable<Customer> customers)
    {{
        return new GetAllCustomersResponse
        {{
            Customers = customers.Select(x => new CustomerResponse
            {{
                Id = x.Id.Value,
                Email = x.Email.Value,
                Username = x.Username.Value,
                FullName = x.FullName.Value,
                DateOfBirth = x.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
            }})
        }};
    }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
