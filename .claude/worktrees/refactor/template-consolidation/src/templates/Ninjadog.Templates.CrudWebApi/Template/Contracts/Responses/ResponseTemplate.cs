namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Responses;

/// <summary>
/// This template generates the response for a given entity.
/// </summary>
public sealed class ResponseTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "Response";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Responses";
        var fileName = $"{st.ClassModelResponse}.cs";

        var content =
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelResponse}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
