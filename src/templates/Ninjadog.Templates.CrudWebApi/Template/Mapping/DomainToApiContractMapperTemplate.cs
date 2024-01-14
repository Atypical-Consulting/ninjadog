// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

public sealed class DomainToApiContractMapperTemplate
    : NinjadogTemplate
{
    public override string GenerateOne(
        NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        var ns = $"{rootNamespace}.Mapping";

        var toModelResponseMethods = string.Join("\n", entities.Select(GenerateToModelResponseMethods));
        var toModelsResponseMethods = string.Join("\n", entities.Select(GenerateToModelsResponseMethods));

        return DefaultCodeLayout(
            $$"""

              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Domain;

              {{WriteFileScopedNamespace(ns)}}

              public static class DomainToApiContractMapper
              {
                  {{toModelsResponseMethods}}
                  {{toModelResponseMethods}}
              }
              """);
    }

    private static string GenerateToModelResponseMethods(NinjadogEntityWithKey entity)
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
                case "System.DateOnly":
                    sb.Append(".ToDateTime(TimeOnly.MinValue)");
                    break;
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }
        }

        return $$"""

                     public static {{st.ClassModelResponse}} {{st.MethodToModelResponse}}(this {{st.Model}} {{st.VarModel}})
                     {
                         return new {{st.ClassModelResponse}}
                         {
                             {{sb}}
                         };
                     }
                 """;
    }

    private static string GenerateToModelsResponseMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var modelProperties = entity.Properties.FromKeys();

        IndentedStringBuilder sb = new(4);

        for (var i = 0; i < modelProperties.Count; i++)
        {
            var isLastItem = i == modelProperties.Count - 1;

            var p = modelProperties[i];

            var baseTypeName = p.Type;
            var isValueOf = baseTypeName is "ValueOf";
            var valueOfArgument = p.Type ?? "";

            sb.Append($"{p.Key} = x.{p.Key}");

            var realType = p.Type.ToString();

            if (isValueOf)
            {
                sb.Append(".Value");
                realType = valueOfArgument;
            }

            switch (realType)
            {
                case "System.DateOnly":
                    sb.Append(".ToDateTime(TimeOnly.MinValue)");
                    break;
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }
        }

        return $$"""

                     public static {{st.ClassGetAllModelsResponse}} {{st.MethodToModelsResponse}}(this IEnumerable<{{st.Model}}> {{st.VarModels}})
                     {
                         return new {{st.ClassGetAllModelsResponse}}
                         {
                             {{st.Models}} = {{st.VarModels}}.Select(x => new {{st.ClassModelResponse}}
                             {
                                 {{sb}}
                             })
                         };
                     }
                 """;
    }
}
