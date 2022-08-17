using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Validation;

[Generator]
public sealed class CreateRequestValidatorGenerator : IIncrementalGenerator
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
            string className = $"Create{sv.Pascal}RequestValidator";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Validation" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using FastEndpoints;
using FluentValidation;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial class {_.ClassCreateModelRequestValidator} : Validator<{_.ClassCreateModelRequest}>
    {{
        public {_.ClassCreateModelRequestValidator}()
        {{
            // TODO: Generate rules for properties
            // RuleFor(x => x.FullName).NotEmpty();
            // RuleFor(x => x.Email).NotEmpty();
            // RuleFor(x => x.Username).NotEmpty();
            // RuleFor(x => x.DateOfBirth).NotEmpty();
        }}
    }}
{(ns is null ? null : @"}
")}";

        return Utilities.DefaultCodeLayout(code);
    }
}
