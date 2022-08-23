using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Validation;

[Generator]
public sealed class UpdateRequestValidatorGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            StringTokens st = new(type.Name);
            var className = $"Update{st.Model}RequestValidator";

            context.AddSource(
                $"{GetRootNamespace(type)}.Validation.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Validation" : null;

        StringTokens _ = new(type.Name);


        // StringTokens st = new(type.Name);
        // string classNameUpdateRequestValidator = $"Update{st.Model}RequestValidator";
        // string classNameUpdateRequest = $"Update{st.Model}Request";

        var code = @$"
using {rootNs}.Contracts.Requests;
using FastEndpoints;
using FluentValidation;

{WriteFileScopedNamespace(ns)}

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

        return DefaultCodeLayout(code);
    }
}
