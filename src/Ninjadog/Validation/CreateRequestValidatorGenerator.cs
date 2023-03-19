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

        var code = $$"""

            using {{rootNs}}.Contracts.Requests;
            using FastEndpoints;
            using FluentValidation;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassCreateModelRequestValidator}} : Validator<{{st.ClassCreateModelRequest}}>
            {
                public {{st.ClassCreateModelRequestValidator}}()
                {
                    {{GenerateValidationRules(typeContext)}}
                }
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateValidationRules(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        var properties = typeContext.PropertyContexts;

        IndentedStringBuilder sb = new();

        sb.IncrementIndent().IncrementIndent();

        foreach (var context in properties.Where(context => !context.IsId))
        {
            sb.AppendLine($"RuleFor(x => x.{context.Name})");
            sb.IncrementIndent();
            sb.AppendLine(".NotEmpty()");
            sb.AppendLine($".WithMessage(\"{context.Name} is required!\");");
            sb.DecrementIndent();

            if (!context.IsLast)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
