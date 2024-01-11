namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;

public class DtoTemplate : NinjadogTemplate
{
    protected override string GetClassName(StringTokens st)
    {
        return $"{st.Model}Dto";
    }

    protected override string GetSubNamespace(StringTokens st)
    {
        return "Contracts.Data";
    }

    public override IEnumerable<string?> GenerateMultipleFiles(NinjadogSettings ninjadogSettings)
    // TODO: The TemplateContext should be passed from a property on the NinjadogTemplateFile base class
    {
        var entities = ninjadogSettings.Entities.FromKeys();
        var config = ninjadogSettings.Config;

        foreach (var entity in entities)
        {
            var st = entity.GetStringTokens();
            var rootNs = config.RootNamespace;
            var ns = $"{rootNs}.Contracts.Data";

            var properties = entity
                .Properties
                .FromKeys()
                .Select(GenerateDtoProperties)
                .Aggregate((x, y) => $"{x}\n{y}");

            var code =
                $$"""

                  using System.Collections.Generic;
                  using {{rootNs}}.Database;
                  using Dapper;

                  {{TemplateUtilities.WriteFileScopedNamespace(ns)}}

                  public partial class {{st.ClassModelDto}}
                  {
                  {{properties}}
                  }
                  """;

            yield return TemplateUtilities.DefaultCodeLayout(code);
        }
    }

    private static string GenerateDtoProperties(NinjadogEntityPropertyWithKey p)
    {
        IndentedStringBuilder sb = new(1);

        sb.Append($"public {p.Type} {p.Key} {{ get; init; }}");

        if (p.Type == "string")
        {
            sb.Append(" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
