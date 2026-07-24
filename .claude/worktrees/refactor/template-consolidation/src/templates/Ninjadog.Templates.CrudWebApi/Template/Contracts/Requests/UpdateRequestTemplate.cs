namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// This template generates the Update request for a given entity.
/// </summary>
public sealed class UpdateRequestTemplate
    : PropertyRequestTemplateBase
{
    /// <inheritdoc />
    public override string Name => "UpdateRequest";

    /// <inheritdoc />
    protected override string GetClassName(StringTokens st)
    {
        return st.ClassUpdateModelRequest;
    }

    /// <inheritdoc />
    protected override string GetActionVerb()
    {
        return "update";
    }

    /// <inheritdoc />
    protected override bool ShouldExcludeAutoKey()
    {
        return false;
    }
}
