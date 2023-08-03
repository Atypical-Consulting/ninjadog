namespace Ninjadog.Contracts.Data;

[Generator]
public sealed class DtoGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"{st.Model}Dto",
            GenerateCode,
            "Contracts.Data");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;
        var type = typeContext.Type;

        var modelProperties = GetPropertiesWithGetSet(type).ToArray();
        var properties = string.Join("\n", modelProperties.Select(GenerateDtoProperties));

        var code = $$"""

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

    private static string GenerateDtoProperties(IPropertySymbol p)
    {
        IndentedStringBuilder sb = new();
        sb.Indent();

        var baseTypeName = p.Type.BaseType?.Name;
        var isValueOf = baseTypeName is "ValueOf";
        var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";

        var realType = isValueOf
            ? valueOfArgument
            : p.Type.ToString();

        var propertyType = realType switch
        {
            "System.Guid" => "string",
            "System.DateOnly" => "DateTime",
            _ => realType
        };

        sb.Append($"public {propertyType} {p.Name} {{ get; init; }}");

        if (propertyType == "string")
        {
            sb.Append(" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
