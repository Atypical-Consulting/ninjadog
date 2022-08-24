namespace Ninjadog.Validation;

[Generator]
public sealed class CreateRequestValidatorGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Create{st.Model}RequestValidator",
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

public partial class {st.ClassCreateModelRequestValidator} : Validator<{st.ClassCreateModelRequest}>
{{
    public {st.ClassCreateModelRequestValidator}()
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
