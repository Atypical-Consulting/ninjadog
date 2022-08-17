using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class DomainToApiContractMapperGenerator : IIncrementalGenerator
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

        const string className = "DomainToApiContractMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ImmutableArray<ITypeSymbol> models)
    {
        var rootNs = Utilities.GetRootNamespace(models[0]);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        var toModelResponseMethods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelResponseMethods));

        var toModelsResponseMethods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelsResponseMethods));

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Domain;

{Utilities.WriteFileScopedNamespace(ns)}

public static class DomainToApiContractMapper
{{
    {toModelsResponseMethods}
    {toModelResponseMethods}
}}";

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateToModelResponseMethods(ITypeSymbol type)
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
                case "System.DateOnly":
                    sb.Append(".ToDateTime(TimeOnly.MinValue)");
                    break;
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }
        }

        return @$"
    public static {_.ClassModelResponse} {_.MethodToModelResponse}(this {_.Model} {_.VarModel})
    {{
        return new {_.ClassModelResponse}
        {{
{sb.ToString()}
        }};
    }}";
    }

    private static string GenerateToModelsResponseMethods(ITypeSymbol type)
    {
        StringTokens _ = new(type.Name);
        var modelProperties = Utilities.GetPropertiesWithGetSet(type).ToArray();

        IndentedStringBuilder sb = new();

        sb.IncrementIndent();
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

            sb.Append($"{p.Name} = x.{p.Name}");

            var realType = p.Type.ToString();

            if (isValueOf)
            {
                sb.Append($".Value");
                realType = valueOfArgument;
            }

            switch (realType)
            {
                case "System.DateOnly":
                    sb.Append(".ToDateTime(TimeOnly.MinValue)");
                    break;
            }

            if (!isLastItem)
            {
                sb.AppendLine(",");
            }
        }

        return @$"
    public static {_.ClassGetAllModelsResponse} {_.MethodToModelsResponse}(this IEnumerable<{_.Model}> {_.VarModels})
    {{
        return new {_.ClassGetAllModelsResponse}
        {{
            {_.Models} = {_.VarModels}.Select(x => new {_.ClassModelResponse}
            {{
{sb.ToString()}
            }})
        }};
    }}";
    }
}
