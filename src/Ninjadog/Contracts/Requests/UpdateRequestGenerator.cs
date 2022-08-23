using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Contracts.Requests;

[Generator]
public sealed class UpdateRequestGenerator : NinjadogBaseGenerator
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
            var className = $"Update{st.Model}Request";

            context.AddSource(
                $"{GetRootNamespace(type)}.Contracts.Requests.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Requests" : null;

        StringTokens _ = new(type.Name);
        var modelProperties = GetPropertiesWithGetSet(type).ToArray();

        var properties = string.Join(
            Environment.NewLine,
            modelProperties.Select(GenerateProperties));

        var code = @$"
{WriteFileScopedNamespace(ns)}

public partial class {_.ClassUpdateModelRequest}
{{
{properties}
}}";

        return DefaultCodeLayout(code);
    }

    private static string GenerateProperties(IPropertySymbol p)
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

        if (propertyType is "string" && p.Name is not "Id")
        {
            sb.Append($" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
