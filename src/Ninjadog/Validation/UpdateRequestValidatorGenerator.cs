namespace Ninjadog.Validation;

[Generator]
public sealed class UpdateRequestValidatorGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Update{st.Model}RequestValidator",
            GenerateCode,
            "Validation");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = @$"
using {rootNs}.Contracts.Requests;
using FastEndpoints;
using FluentValidation;

{WriteFileScopedNamespace(ns)}

public partial class {st.ClassUpdateModelRequestValidator} : Validator<{st.ClassUpdateModelRequest}>
{{
    public {st.ClassUpdateModelRequestValidator}()
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
