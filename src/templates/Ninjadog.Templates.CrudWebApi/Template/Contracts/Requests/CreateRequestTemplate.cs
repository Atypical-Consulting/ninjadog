namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// This template generates the Create request for a given entity.
/// </summary>
public sealed class CreateRequestTemplate
    : PropertyRequestTemplateBase
{
    /// <inheritdoc />
    public override string Name => "CreateRequest";

    /// <inheritdoc />
    protected override string GetClassName(StringTokens st)
    {
        return st.ClassCreateModelRequest;
    }

    /// <inheritdoc />
    protected override string GetActionVerb()
    {
        return "create";
    }

    /// <inheritdoc />
    protected override bool ShouldExcludeAutoKey()
    {
        return true;
    }
}
