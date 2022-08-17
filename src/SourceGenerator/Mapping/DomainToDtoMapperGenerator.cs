using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class DomainToDtoMapperGenerator : IIncrementalGenerator
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

        const string className = "DomainToDtoMapperGenerator";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ImmutableArray<ITypeSymbol> models)
    {
        var rootNs = Utilities.GetRootNamespace(models[0]);
        var ns = rootNs is not null ? $"{rootNs}.Mapping" : null;

        var methods = string.Join(
            Environment.NewLine,
            models.Select(GenerateToModelDtoMethods));

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Domain;

{Utilities.WriteFileScopedNamespace(ns)}

public static class DomainToDtoMapper
{{
    {methods}
}}";

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateToModelDtoMethods(ITypeSymbol type)
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
{sb.ToString()}
        }};
    }}";
    }
}
