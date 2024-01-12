// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Settings.Extensions;

public sealed record NinjadogEntityPropertyWithKey(
    string Key, NinjadogEntityProperty Property)
    : NinjadogEntityProperty(Property)
{
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
