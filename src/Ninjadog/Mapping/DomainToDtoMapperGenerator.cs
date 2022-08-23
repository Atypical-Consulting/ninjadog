using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Mapping;

[Generator]
public sealed class DomainToDtoMapperGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        var type = models[0];
        const string className = "DomainToDtoMapperGenerator";

        context.AddSource(
            $"{GetRootNamespace(type)}.Mapping.{className}.g.cs",
            GenerateCode(models));
    }

    private static string GenerateCode(ImmutableArray<ITypeSymbol> models)
    {
        var rootNs = GetRootNamespace(models[0]);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        var methods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelDtoMethods));

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Domain;

{WriteFileScopedNamespace(ns)}

public static class DomainToDtoMapper
{{
    {methods}
}}";

        return DefaultCodeLayout(code);
    }

    private static string GenerateToModelDtoMethods(ITypeSymbol type)
    {
        StringTokens _ = new(type.Name);
        var modelProperties = GetPropertiesWithGetSet(type).ToArray();

        IndentedStringBuilder sb = new();

        sb.IncrementIndent();
        sb.IncrementIndent();
        sb.IncrementIndent();

        for (var i = 0; i < modelProperties.Length; i++)
        {
            bool isLastItem = i == modelProperties.Length - 1;

            var p = modelProperties[i];

            var baseTypeName = p.Type.BaseType?.Name;
            var isValueOf = baseTypeName is "ValueOf";
            var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";

            sb.Append($"{p.Name} = {_.VarModel}.{p.Name}");

            var realType = p.Type.ToString();

            if (isValueOf)
            {
                sb.Append($".Value");
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

        return @$"
    public static {_.ClassModelDto} {_.MethodToModelDto}(this {_.Model} {_.VarModel})
    {{
        return new {_.ClassModelDto}
        {{
{sb}
        }};
    }}";
    }
}
