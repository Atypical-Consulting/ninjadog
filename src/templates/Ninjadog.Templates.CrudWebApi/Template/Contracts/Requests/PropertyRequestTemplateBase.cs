namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// Base class for request templates that contain all entity properties
/// (e.g., Create and Update requests).
/// </summary>
public abstract class PropertyRequestTemplateBase
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Requests";
        var className = GetClassName(st);
        var fileName = $"{className}.cs";
        var actionVerb = GetActionVerb();

        var content =
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              /// <summary>
              ///     Request to {{actionVerb}} a {{st.Model}}.
              /// </summary>
              public partial class {{className}}
              {
              {{entity.GenerateMemberProperties(excludeAutoKey: ShouldExcludeAutoKey())}}
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }

    /// <summary>
    /// Gets the request class name from the string tokens.
    /// </summary>
    /// <param name="st">The string tokens for the entity.</param>
    /// <returns>The request class name.</returns>
    protected abstract string GetClassName(StringTokens st);

    /// <summary>
    /// Gets the action verb for the XML doc comment (e.g., "create", "update").
    /// </summary>
    /// <returns>The action verb.</returns>
    protected abstract string GetActionVerb();

    /// <summary>
    /// Gets whether auto-key properties should be excluded from the request.
    /// </summary>
    /// <returns><see langword="true"/> to exclude auto-key properties; otherwise, <see langword="false"/>.</returns>
    protected abstract bool ShouldExcludeAutoKey();
}
