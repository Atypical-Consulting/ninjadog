using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Validation;

public sealed class CreateRequestValidatorTemplate
    : NinjadogTemplate
{
    public override string GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Validation";

        return DefaultCodeLayout(
            $$"""

              using {{rootNamespace}}.Contracts.Requests;
              using FastEndpoints;
              using FluentValidation;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassCreateModelRequestValidator}} : Validator<{{st.ClassCreateModelRequest}}>
              {
                  public {{st.ClassCreateModelRequestValidator}}()
                  {
                      {{GenerateValidationRules(entity)}}
                  }
              }
              """);
    }

    private static string GenerateValidationRules(NinjadogEntityWithKey entity)
    {
        var properties = entity.Properties;

        IndentedStringBuilder sb = new(2);

        foreach (var (propertyName, propertyValue) in properties)
        {
            if (propertyValue.IsKey)
            {
                continue;
            }

            sb.AppendLine($"RuleFor(x => x.{propertyName})");
            sb.IncrementIndent();
            sb.AppendLine(".NotEmpty()");
            sb.AppendLine($".WithMessage(\"{propertyName} is required!\");");
            sb.DecrementIndent();

            if (propertyName != properties.Last().Key)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
