// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class UpdateRequestGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Update{st.Model}Request",
            GenerateCode,
            "Contracts.Requests");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var type = typeContext.Type;

        var modelProperties = GetPropertiesWithGetSet(type).ToArray();
        var properties = string.Join("\n", modelProperties.Select(GenerateProperties));

        var code = $$"""

            {{WriteFileScopedNamespace(ns)}}

            /// <summary>
            ///     Request to update a {{st.Model}}.
            /// </summary>
            public partial class {{st.ClassUpdateModelRequest}}
            {
            {{properties}}
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateProperties(IPropertySymbol p)
    {
        IndentedStringBuilder sb = new(1);

        var baseTypeName = p.Type.BaseType?.Name;
        var isValueOf = baseTypeName is "ValueOf";
        var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";

        var realType = isValueOf
            ? valueOfArgument
            : p.Type.ToString();

        var propertyType = realType switch
        {
            "System.DateOnly" => "DateTime",
            _ => realType
        };

        sb.Append($"public {propertyType} {p.Name} {{ get; init; }}");

        if (propertyType is "string" && p.Name is not "Id")
        {
            sb.Append(" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
