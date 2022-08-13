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
            var typeNamespace = type.ContainingNamespace.IsGlobalNamespace
                ? null
                : $"{type.ContainingNamespace}.";

            context.AddSource($"{typeNamespace}{type.Name}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Validation" : null;
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using {rootNs}.Contracts.Requests;
using FastEndpoints;
using FluentValidation;
    
{(ns is null ? null : $@"namespace {ns}
{{")}
    public class Update{name}RequestValidator : Validator<Update{name}Request>
    {{
        public Update{name}RequestValidator()
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
    }
}