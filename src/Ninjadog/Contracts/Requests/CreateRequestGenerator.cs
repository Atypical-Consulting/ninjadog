namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class CreateRequestGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"Create{st.Model}Request",
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
            ///     Request to create a {{st.Model}}.
            /// </summary>
            public partial class {{st.ClassCreateModelRequest}}
            {
            {{properties}}
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateProperties(IPropertySymbol p)
    {
        if (p.Name is "Id")
        {
            return "";
        }

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
