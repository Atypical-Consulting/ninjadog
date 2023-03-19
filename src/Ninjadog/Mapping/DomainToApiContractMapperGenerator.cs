namespace Ninjadog.Mapping;

[Generator]
public sealed class DomainToApiContractMapperGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            "DomainToApiContractMapperGenerator",
            GenerateCode,
            "Mapping");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;
        var rootNs = typeContext.RootNamespace;

        var toModelResponseMethods = string.Join(
            Environment.NewLine,
            typeContexts.Select(GenerateToModelResponseMethods));

        var toModelsResponseMethods = string.Join(
            Environment.NewLine,
            typeContexts.Select(GenerateToModelsResponseMethods));

        var code = $$"""

            using {{rootNs}}.Contracts.Responses;
            using {{rootNs}}.Domain;

            {{WriteFileScopedNamespace(ns)}}

            public static class DomainToApiContractMapper
            {
                {{toModelsResponseMethods}}
                {{toModelResponseMethods}}
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateToModelResponseMethods(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        var type = typeContext.Type;

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

            sb.Append($"{p.Name} = {st.VarModel}.{p.Name}");

            var realType = p.Type.ToString();

            if (isValueOf)
            {
                sb.Append(".Value");
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

        return $$"""

                public static {{st.ClassModelResponse}} {{st.MethodToModelResponse}}(this {{st.Model}} {{st.VarModel}})
                {
                    return new {{st.ClassModelResponse}}
                    {
                        {{sb}}
                    };
                }
            """;
    }

    private static string GenerateToModelsResponseMethods(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        var type = typeContext.Type;

        var modelProperties = GetPropertiesWithGetSet(type).ToArray();

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
                sb.Append(".Value");
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

        return $$"""

                public static {{st.ClassGetAllModelsResponse}} {{st.MethodToModelsResponse}}(this IEnumerable<{{st.Model}}> {{st.VarModels}})
                {
                    return new {{st.ClassGetAllModelsResponse}}
                    {
                        {{st.Models}} = {{st.VarModels}}.Select(x => new {{st.ClassModelResponse}}
                        {
                            {{sb}}
                        })
                    };
                }
            """;
    }
}
