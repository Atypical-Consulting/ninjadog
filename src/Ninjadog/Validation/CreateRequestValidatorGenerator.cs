using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Ninjadog.Validation;

[Generator]
public sealed class CreateRequestValidatorGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modelTypes = Utilities.CollectNinjadogModelTypes(context);

        context.RegisterSourceOutput(modelTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Validation";

            StringVariations sv = new(type.Name);
            var className = $"Create{sv.Pascal}RequestValidator";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Validation" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using FastEndpoints;
using FluentValidation;

{Utilities.WriteFileScopedNamespace(ns)}

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
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
