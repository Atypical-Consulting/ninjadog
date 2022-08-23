using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Contracts.Responses;

[Generator]
public sealed class ResponseGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            StringTokens st = new(type.Name);
            var className = $"{st.Model}Response";

            context.AddSource(
                $"{GetRootNamespace(type)}.Contracts.Responses.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Responses" : null;

        StringTokens _ = new(type.Name);
        var modelProperties = GetPropertiesWithGetSet(type).ToArray();

        var properties = string.Join(
            Environment.NewLine,
            modelProperties.Select(property => GenerateDtoProperties(property)));

        var code = @$"
{WriteFileScopedNamespace(ns)}

public partial class {_.ClassModelResponse}
{{
{properties}
}}";

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
            "System.DateOnly" => "DateTime",
            _ => realType
        };

        sb.Append($"public {propertyType} {p.Name} {{ get; init; }}");

        if (propertyType == "string")
        {
            sb.Append($" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
