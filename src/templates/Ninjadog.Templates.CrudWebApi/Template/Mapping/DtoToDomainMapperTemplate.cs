// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

public sealed class DtoToDomainMapperTemplate
    : NinjadogTemplate
{
    public override string GenerateOne(
        NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        var ns = $"{rootNamespace}.Mapping";

        var methods = string.Join("\n", entities.Select(GenerateToModelMethods));

        return DefaultCodeLayout(
            $$"""

              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Domain;
              using {{rootNamespace}}.Domain.Common;

              {{WriteFileScopedNamespace(ns)}}

              public static class DtoToDomainMapper
              {
                  {{methods}}
              }
              """);
    }


    private static string GenerateToModelMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var modelProperties = entity.Properties.FromKeys();

        IndentedStringBuilder sb = new(3);

        sb.IncrementIndent(3);

        for (var i = 0; i < modelProperties.Count; i++)
        {
            var isLastItem = i == modelProperties.Count - 1;
            var p = modelProperties[i];
            var baseTypeName = p.Type;
            var isValueOf = baseTypeName is "ValueOf";
            var valueOfArgument = p.Type ?? "";

            sb.Append($"{p.Key} = ");

            if (isValueOf)
            {
                sb.Append($"{p.Type}.From(");
            }

            var realType = isValueOf
                ? valueOfArgument
                : p.Type;

            switch (realType)
            {
                case "System.Guid":
                    sb.Append($"Guid.Parse({st.VarModelDto}.{p.Key})");
                    break;
                case "System.DateOnly":
                    sb.Append($"DateOnly.FromDateTime({st.VarModelDto}.{p.Key})");
                    break;
                default:
                    sb.Append($"{st.VarModelDto}.{p.Key}");
                    break;
            }

            if (isValueOf)
            {
                sb.Append(")");
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }
        }

        return $$"""

                     public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassModelDto}} {{st.VarModelDto}})
                     {
                         return new {{st.Model}}
                         {
                             {{sb}}
                         };
                     }
                 """;
    }
}
