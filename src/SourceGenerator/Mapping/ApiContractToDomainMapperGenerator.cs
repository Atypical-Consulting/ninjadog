using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class ApiContractToDomainMapperGenerator : IIncrementalGenerator
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

        const string className = "ApiContractToDomainMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ImmutableArray<ITypeSymbol> models)
    {
        var rootNs = Utilities.GetRootNamespace(models[0]);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        var toModelFromCreateMethods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelFromCreateMethods));

        var toModelFromUpdateMethods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelFromUpdateMethods));

        // StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Requests;
using {rootNs}.Domain;
using {rootNs}.Domain.Common;

{Utilities.WriteFileScopedNamespace(ns)}

public static class ApiContractToDomainMapper
{{
    {toModelFromCreateMethods}
    {toModelFromUpdateMethods}
}}";

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateToModelFromCreateMethods(ITypeSymbol type)
    {
        StringTokens _ = new(type.Name);
        var modelProperties = Utilities.GetPropertiesWithGetSet(type).ToArray();

        IndentedStringBuilder sb = new();

        sb.IncrementIndent();
        sb.IncrementIndent();
        sb.IncrementIndent();

        for (var i = 0; i < modelProperties.Length; i++)
        {
            bool isLastItem = i == modelProperties.Length - 1;

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
                    sb.Append($"Guid.NewGuid()");
                    break;
                case "System.DateOnly":
                    sb.Append($"DateOnly.FromDateTime(request.{p.Name})");
                    break;
                default:
                    sb.Append($"request.{p.Name}");
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
    public static {_.Model} {_.MethodToModel}(this {_.ClassCreateModelRequest} request)
    {{
        return new {_.Model}
        {{
{sb.ToString()}
        }};
    }}";
    }

    private static string GenerateToModelFromUpdateMethods(ITypeSymbol type)
    {
        StringTokens _ = new(type.Name);
        var modelProperties = Utilities.GetPropertiesWithGetSet(type).ToArray();

        IndentedStringBuilder sb = new();

        sb.IncrementIndent();
        sb.IncrementIndent();
        sb.IncrementIndent();

        for (var i = 0; i < modelProperties.Length; i++)
        {
            bool isLastItem = i == modelProperties.Length - 1;

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
                case "System.DateOnly":
                    sb.Append($"DateOnly.FromDateTime(request.{p.Name})");
                    break;
                default:
                    sb.Append($"request.{p.Name}");
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
    public static {_.Model} {_.MethodToModel}(this {_.ClassUpdateModelRequest} request)
    {{
        return new {_.Model}
        {{
{sb.ToString()}
        }};
    }}";
    }
}
