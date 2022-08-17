using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Contracts.Requests;

[Generator]
public sealed class CreateRequestGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modelTypes = Utilities.CollectNinjadogModelTypes(context);

        context.RegisterSourceOutput(modelTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Contracts.Requests";

            StringVariations sv = new(type.Name);
            var className = $"Create{sv.Pascal}Request";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Contracts.Requests" : null;

        StringTokens _ = new(type.Name);
        var modelProperties = Utilities.GetPropertiesWithGetSet(type).ToArray();

        var properties = string.Join(
            Environment.NewLine,
            modelProperties.Select(property => GenerateProperties(property, _)));

        var code = @$"
{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassCreateModelRequest}
{{
{properties}
}}";

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateProperties(IPropertySymbol p, StringTokens _)
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
            sb.Append($" = default!;");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
