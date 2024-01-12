// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using static Ninjadog.Templates.TemplateUtilities;

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;

public class DtoTemplate : NinjadogTemplate
{
    public override IEnumerable<string?> GenerateOneToMany(NinjadogSettings ninjadogSettings)
    {
        var entities = ninjadogSettings.Entities.FromKeys();
        var rootNs = ninjadogSettings.Config.RootNamespace;

        foreach (var entity in entities)
        {
            yield return GenerateDto(entity, rootNs);
        }
    }

    private static string GenerateDto(NinjadogEntityWithKey entity, string rootNs)
    {
        var st = entity.GetStringTokens();
        var ns = $"{rootNs}.Contracts.Data";

        return DefaultCodeLayout(
            $$"""

              using System.Collections.Generic;
              using {{rootNs}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelDto}}
              {
              {{GenerateDtoProperties(entity)}}
              }
              """);
    }

    private static string GenerateDtoProperties(NinjadogEntityWithKey entity)
    {
        return entity
            .Properties
            .FromKeys()
            .Select(GenerateDtoProperty)
            .Aggregate((x, y) => $"{x}\n{y}");
    }

    private static string GenerateDtoProperty(NinjadogEntityPropertyWithKey property)
    {
        var propertyType = property.Type;
        var propertyKey = property.Key;

        IndentedStringBuilder sb = new(1);

        sb.Append($"public {propertyType} {propertyKey} {{ get; init; }}");

        if (propertyType == "string")
        {
            sb.Append(" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
