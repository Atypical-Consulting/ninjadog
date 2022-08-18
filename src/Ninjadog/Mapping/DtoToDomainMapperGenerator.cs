using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Ninjadog.Mapping;

[Generator]
public sealed class DtoToDomainMapperGenerator : IIncrementalGenerator
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

        var type = models[0];
        var code = GenerateCode(models);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Mapping";

        const string className = "DtoToDomainMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ImmutableArray<ITypeSymbol> models)
    {
        var rootNs = Utilities.GetRootNamespace(models[0]);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        var methods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelMethods));

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Domain;
using {rootNs}.Domain.Common;

{Utilities.WriteFileScopedNamespace(ns)}

public static class DtoToDomainMapper
{{
    {methods}
}}";

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateToModelMethods(ITypeSymbol type)
    {
        StringTokens _ = new(type.Name);
        var modelProperties = Utilities.GetPropertiesWithGetSet(type).ToArray();

        IndentedStringBuilder sb = new();

        sb.IncrementIndent();
        sb.IncrementIndent();
        sb.IncrementIndent();

        for (var i = 0; i < modelProperties.Length; i++)
        {
            var isLastItem = i == modelProperties.Length - 1;

            var p = modelProperties[i];

            if (p.IsReadOnly)
            {
                continue;
            }

            var baseTypeName = p.Type.BaseType?.Name;
            var isValueOf = baseTypeName is "ValueOf";
            var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";

            sb.Append($"{p.Name} = ");

            if (isValueOf)
            {
                sb.Append($"{p.Type}.From(");
            }

            var realType = isValueOf
                ? valueOfArgument
                : p.Type.ToString();

            switch (realType)
            {
                case "System.Guid":
                    sb.Append($"Guid.Parse({_.VarModelDto}.{p.Name})");
                    break;
                case "System.DateOnly":
                    sb.Append($"DateOnly.FromDateTime({_.VarModelDto}.{p.Name})");
                    break;
                default:
                    sb.Append($"{_.VarModelDto}.{p.Name}");
                    break;
            }

            if (isValueOf)
            {
                sb.Append(")");
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }
        }

        return @$"
    public static {_.Model} {_.MethodToModel}(this {_.ClassModelDto} {_.VarModelDto})
    {{
        return new {_.Model}
        {{
{sb}
        }};
    }}";
    }
}
