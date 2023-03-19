namespace Ninjadog.Mapping;

[Generator]
public sealed class ApiContractToDomainMapperGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            "ApiContractToDomainMapperGenerator",
            GenerateCode,
            "Mapping");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;
        var rootNs = typeContext.RootNamespace;

        var toModelFromCreateMethods = string.Join(
            Environment.NewLine,
            typeContexts.Select(GenerateToModelFromCreateMethods));

        var toModelFromUpdateMethods = string.Join(
            Environment.NewLine,
            typeContexts.Select(GenerateToModelFromUpdateMethods));

        var code = $$"""

            using {{rootNs}}.Contracts.Requests;
            using {{rootNs}}.Domain;
            using {{rootNs}}.Domain.Common;

            {{WriteFileScopedNamespace(ns)}}

            public static class ApiContractToDomainMapper
            {
                {{toModelFromCreateMethods}}
                {{toModelFromUpdateMethods}}
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateToModelFromCreateMethods(TypeContext typeContext)
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
                    sb.Append("Guid.NewGuid()");
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

        return $$"""

                public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassCreateModelRequest}} request)
                {
                    return new {{st.Model}}
                    {
                        {{sb}}
                    };
                }
            """;
    }

    private static string GenerateToModelFromUpdateMethods(TypeContext typeContext)
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

        return $$"""

                public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassUpdateModelRequest}} request)
                {
                    return new {{st.Model}}
                    {
                        {{sb}}
                    };
                }
            """;
    }
}
