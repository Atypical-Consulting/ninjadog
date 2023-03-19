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

        var code = $$"""

            using {{rootNs}}.Contracts.Requests;
            using FastEndpoints;
            using FluentValidation;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassUpdateModelRequestValidator}} : Validator<{{st.ClassUpdateModelRequest}}>
            {
                public {{st.ClassUpdateModelRequestValidator}}()
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
            if (!context.IsLast)
            {
                sb.AppendLine($"RuleFor(x => x.{context.Name}).NotEmpty();");
            }
            else
            {
                sb.Append($"RuleFor(x => x.{context.Name}).NotEmpty();");
            }
        }

        return sb.ToString();
    }
}
