// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities.Properties;

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Represents a Ninjadog entity property along with its key.
/// This record extends <see cref="NinjadogEntityProperty"/> by associating it with a specific key,
/// providing additional context and utility for template generation.
/// </summary>
/// <param name="Key">The key associated with the property.</param>
/// <param name="Property">The entity property.</param>
public sealed record NinjadogEntityPropertyWithKey(
    string Key, NinjadogEntityProperty Property)
    : NinjadogEntityProperty(Property)
{
    /// <summary>
    /// Generates the member property definition for the associated entity property.
    /// This method produces the C# code representation of the property, including its type and accessors.
    /// </summary>
    /// <returns>A <see cref="string"/> containing the C# code for the member property.</returns>
    public string GenerateMemberProperty()
    {
        // var baseTypeName = p.Type.BaseType?.Name;
        // var isValueOf = baseTypeName is "ValueOf";
        // var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";
        //
        // var realType = isValueOf
        //     ? valueOfArgument
        //     : p.Type.ToString();
        //
        // var propertyType = realType switch
        // {
        //     "System.Guid" => "string",
        //     "System.DateOnly" => "DateTime",
        //     _ => realType
        // };

        IndentedStringBuilder sb = new(1);

        sb.Append($"public {Type} {Key} {{ get; init; }}");

        if (Type == "string")
        {
            sb.Append(" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
