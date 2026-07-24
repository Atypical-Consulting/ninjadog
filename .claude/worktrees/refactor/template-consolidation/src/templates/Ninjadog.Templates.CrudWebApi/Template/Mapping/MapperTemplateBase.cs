namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

/// <summary>
/// Base class for mapper templates that generate static extension method classes.
/// Subclasses provide the class name, using directives, and method generation logic.
/// </summary>
public abstract class MapperTemplateBase
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        var ns = $"{rootNamespace}.Mapping";
        var className = Name;
        var fileName = $"{className}.cs";

        var methods = GenerateMethods(entities);

        var content =
            $$"""

              {{GenerateUsings(rootNamespace)}}

              {{WriteFileScopedNamespace(ns)}}

              public static class {{className}}
              {
                  {{methods}}
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }

    /// <summary>
    /// Generates the using directives for this mapper.
    /// </summary>
    /// <param name="rootNamespace">The root namespace of the project.</param>
    /// <returns>The using directive lines.</returns>
    protected abstract string GenerateUsings(string rootNamespace);

    /// <summary>
    /// Generates all mapper methods for the given entities.
    /// </summary>
    /// <param name="entities">The list of entities to generate methods for.</param>
    /// <returns>The generated method code.</returns>
    protected abstract string GenerateMethods(List<NinjadogEntityWithKey> entities);
}
