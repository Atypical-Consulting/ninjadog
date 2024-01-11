// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Configuration;

namespace Ninjadog;

/// <summary>
/// A sample source generator that creates C# classes based on the NinjadogSettings.
/// When using a simple json file as a baseline, we can create a non-incremental source generator.
/// </summary>
[Generator]
public class NinjadogDtoGenerator
    : NinjadogMultipleFilesSourceGeneratorBase
{
    protected override string GenerateCode(
        NinjadogConfiguration config,
        NinjadogEntityWithKey entity)
    {
        var st = entity.GetStringTokens();
        var rootNs = config.RootNamespace;
        var ns = $"{rootNs}.Contracts.Data";

        // var properties = string
            // .Join("\n", entity.Properties.FromKeys().Select(GenerateDtoProperties));

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

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelDto}}
              {
              {{properties}}
              }
              """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateDtoProperties(NinjadogEntityPropertyWithKey p)
    {
        IndentedStringBuilder sb = new(1);

        // var baseTypeName = p.Type;
        // var isValueOf = baseTypeName is "ValueOf";
        // var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";
        //
        // var realType = isValueOf
        //     ? valueOfArgument
        //     : p.Type;
        //
        // var propertyType = realType switch
        // {
        //     "System.Guid" => "string",
        //     "System.DateOnly" => "DateTime",
        //     _ => realType
        // };

        sb.Append($"public {p.Type} {p.Key} {{ get; init; }}");

        if (p.Type == "string")
        {
            sb.Append(" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
