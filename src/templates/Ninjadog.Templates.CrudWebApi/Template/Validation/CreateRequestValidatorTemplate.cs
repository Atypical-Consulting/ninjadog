namespace Ninjadog.Templates.CrudWebAPI.Template.Validation;

/// <summary>
/// This template generates the validation rules for the create request of a given entity.
/// </summary>
public sealed class CreateRequestValidatorTemplate
    : ValidatorTemplateBase
{
    /// <inheritdoc />
    public override string Name => "CreateRequestValidator";

    /// <inheritdoc />
    protected override string GetRequestClassName(StringTokens st)
    {
        return st.ClassCreateModelRequest;
    }

    /// <inheritdoc />
    protected override string GetValidatorClassName(StringTokens st)
    {
        return st.ClassCreateModelRequestValidator;
    }
}
