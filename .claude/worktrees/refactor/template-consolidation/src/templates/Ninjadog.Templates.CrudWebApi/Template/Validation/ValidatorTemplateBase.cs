namespace Ninjadog.Templates.CrudWebAPI.Template.Validation;

/// <summary>
/// Base class for validator templates that share the same structure.
/// Subclasses provide the request and validator class names.
/// </summary>
public abstract class ValidatorTemplateBase
    : NinjadogTemplate
{
    /// <inheritdoc/>
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Validation";
        var validatorClassName = GetValidatorClassName(st);
        var requestClassName = GetRequestClassName(st);
        var fileName = $"{validatorClassName}.cs";

        var content =
            $$"""

              using {{rootNamespace}}.Contracts.Requests;
              using FastEndpoints;
              using FluentValidation;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{validatorClassName}} : Validator<{{requestClassName}}>
              {
                  public {{validatorClassName}}()
                  {
              {{ValidationRuleGenerator.GenerateValidationRules(entity)}}
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }

    /// <summary>
    /// Gets the request class name from the string tokens.
    /// </summary>
    /// <param name="st">The string tokens for the entity.</param>
    /// <returns>The request class name.</returns>
    protected abstract string GetRequestClassName(StringTokens st);

    /// <summary>
    /// Gets the validator class name from the string tokens.
    /// </summary>
    /// <param name="st">The string tokens for the entity.</param>
    /// <returns>The validator class name.</returns>
    protected abstract string GetValidatorClassName(StringTokens st);
}
