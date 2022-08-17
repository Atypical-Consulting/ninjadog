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
        var code = GenerateCode(type, enumerations);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Mapping";

        const string className = "DomainToDtoMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type, ImmutableArray<ITypeSymbol> models)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        var methods = string.Join(
            Environment.NewLine,
            models.Select(GenerateMethodsToModelDto));

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Domain;

{Utilities.WriteFileScopedNamespace(ns)}

public static class DomainToDtoMapper
{{
    {methods}
}}";

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateMethodsToModelDto(ITypeSymbol type)
    {
        StringTokens _ = new(type.Name);

        return @$"
    public static {_.ClassModelDto} {_.MethodToModelDto}(this {_.Model} {_.VarModel})
    {{
        return new {_.ClassModelDto}
        {{
            Id = customer.Id.Value.ToString(),
            Email = customer.Email.Value,
            Username = customer.Username.Value,
            FullName = customer.FullName.Value,
            DateOfBirth = customer.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        }};
    }}";
    }
}
