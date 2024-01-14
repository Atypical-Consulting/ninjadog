// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

public sealed class DomainToDtoMapperTemplate
    : NinjadogTemplate
{
    public override string GenerateOne(
        NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        var ns = $"{rootNamespace}.Mapping";

        var methods = string.Join("\n", entities.Select(GenerateToModelDtoMethods));

        return DefaultCodeLayout(
            $$"""

              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Domain;

              {{WriteFileScopedNamespace(ns)}}

              public static class DomainToDtoMapper
              {
                  {{methods}}
              }
              """);
    }

    private static string GenerateToModelDtoMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var modelProperties = entity.Properties.FromKeys();

        IndentedStringBuilder sb = new(3);

        for (var i = 0; i < modelProperties.Count; i++)
        {
            var isLastItem = i == modelProperties.Count - 1;

            var p = modelProperties[i];

            var baseTypeName = p.Type;
            var isValueOf = baseTypeName is "ValueOf";
            var valueOfArgument = p.Type ?? "";

            sb.Append($"{p.Key} = {st.VarModel}.{p.Key}");

            var realType = p.Type.ToString();

            if (isValueOf)
            {
                sb.Append(".Value");
                realType = valueOfArgument;
            }

            switch (realType)
            {
                case "System.Guid":
                    sb.Append(".ToString()");
                    break;
                case "System.DateOnly":
                    sb.Append(".ToDateTime(TimeOnly.MinValue)");
                    break;
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }

            // Uncomment the following lines for debug...

            // sb.AppendLine($"//   -- type          : {p.Type}");
            // sb.AppendLine($"//   -- base type     : {baseTypeName}");
            //
            // if (isValueOf)
            // {
            //     sb.AppendLine($"//   -- value of      : {valueOfArgument}");
            // }
        }

        return $$"""

                     public static {{st.ClassModelDto}} {{st.MethodToModelDto}}(this {{st.Model}} {{st.VarModel}})
                     {
                         return new {{st.ClassModelDto}}
                         {
                             {{sb}}
                         };
                     }
                 """;
    }
}
