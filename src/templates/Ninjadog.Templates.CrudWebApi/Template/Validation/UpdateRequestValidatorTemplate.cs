using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Validation;

public class UpdateRequestValidatorTemplate
    : NinjadogTemplate
{
    public override string GenerateOneToManyForEntity(
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

              public partial class {{st.ClassUpdateModelRequestValidator}} : Validator<{{st.ClassUpdateModelRequest}}>
              {
                  public {{st.ClassUpdateModelRequestValidator}}()
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

            // Applying validation rules for each property
            sb.AppendLine($"RuleFor(x => x.{propertyName}).NotEmpty();");

            // Check if not the last property to add an extra line break
            if (propertyName != properties.Last().Key)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
