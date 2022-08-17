using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Validation;

[Generator]
public sealed class UpdateRequestValidatorGenerator : IIncrementalGenerator
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
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Validation";

            StringVariations sv = new(type.Name);
            var className = $"Update{sv.Pascal}RequestValidator";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Validation" : null;

        StringTokens _ = new(type.Name);


        // StringVariations sv = new(type.Name);
        // string classNameUpdateRequestValidator = $"Update{sv.Pascal}RequestValidator";
        // string classNameUpdateRequest = $"Update{sv.Pascal}Request";

        var code = @$"
using {rootNs}.Contracts.Requests;
using FastEndpoints;
using FluentValidation;

{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassUpdateModelRequestValidator} : Validator<{_.ClassUpdateModelRequest}>
{{
    public {_.ClassUpdateModelRequestValidator}()
    {{
        // TODO: Generate rules for properties
        // RuleFor(x => x.FullName).NotEmpty();
        // RuleFor(x => x.Email).NotEmpty();
        // RuleFor(x => x.Username).NotEmpty();
        // RuleFor(x => x.DateOfBirth).NotEmpty();
    }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
