namespace Ninjadog.Templates.CrudWebAPI.Template.Validation;

/// <summary>
/// This template generates the validation rules for the update request of a given entity.
/// </summary>
public sealed class UpdateRequestValidatorTemplate
    : ValidatorTemplateBase
{
    /// <inheritdoc />
    public override string Name => "UpdateRequestValidator";

    /// <inheritdoc />
    protected override string GetRequestClassName(StringTokens st)
    {
        return st.ClassUpdateModelRequest;
    }

    /// <inheritdoc />
    protected override string GetValidatorClassName(StringTokens st)
    {
        return st.ClassUpdateModelRequestValidator;
    }
}
