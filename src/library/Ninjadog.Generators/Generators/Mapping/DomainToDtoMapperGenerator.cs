// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Mapping;

[Generator]
public sealed class DomainToDtoMapperGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            "DomainToDtoMapperGenerator",
            GenerateCode,
            "Mapping");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;
        var rootNs = typeContext.RootNamespace;

        var methods = string.Join("\n", typeContexts.Select(GenerateToModelDtoMethods));

        var code = $$"""

            using {{rootNs}}.Contracts.Data;
            using {{rootNs}}.Domain;

            {{WriteFileScopedNamespace(ns)}}

            public static class DomainToDtoMapper
            {
                {{methods}}
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateToModelDtoMethods(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        var type = typeContext.Type;

        var modelProperties = GetPropertiesWithGetSet(type).ToArray();

        IndentedStringBuilder sb = new(3);

        for (var i = 0; i < modelProperties.Length; i++)
        {
            var isLastItem = i == modelProperties.Length - 1;

            var p = modelProperties[i];

            var baseTypeName = p.Type.BaseType?.Name;
            var isValueOf = baseTypeName is "ValueOf";
            var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";

            sb.Append($"{p.Name} = {st.VarModel}.{p.Name}");

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
