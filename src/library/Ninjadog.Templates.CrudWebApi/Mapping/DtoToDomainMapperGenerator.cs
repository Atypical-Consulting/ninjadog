// namespace Ninjadog.Mapping;
//
// [Generator]
// public sealed class DtoToDomainMapperGenerator : NinjadogIncrementalGeneratorBase
// {
//     /// <inheritdoc />
//     protected override IncrementalGeneratorSetup Setup
//         => new(
//             "DtoToDomainMapperGenerator",
//             GenerateCode,
//             "Mapping");
//
//     private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
//     {
//         var typeContext = typeContexts[0];
//         var ns = typeContext.Ns;
//         var rootNs = typeContext.RootNamespace;
//
//         var methods = string.Join("\n", typeContexts.Select(GenerateToModelMethods));
//
//         var code = $$"""
//
//             using {{rootNs}}.Contracts.Data;
//             using {{rootNs}}.Domain;
//             using {{rootNs}}.Domain.Common;
//
//             {{WriteFileScopedNamespace(ns)}}
//
//             public static class DtoToDomainMapper
//             {
//                 {{methods}}
//             }
//             """;
//
//         return DefaultCodeLayout(code);
//     }
//
//     private static string GenerateToModelMethods(TypeContext typeContext)
//     {
//         var st = typeContext.Tokens;
//         var type = typeContext.Type;
//         var modelProperties = GetPropertiesWithGetSet(type).ToArray();
//
//         IndentedStringBuilder sb = new(3);
//
//         sb.IncrementIndent(3);
//
//         for (var i = 0; i < modelProperties.Length; i++)
//         {
//             var isLastItem = i == modelProperties.Length - 1;
//
//             var p = modelProperties[i];
//
//             if (p.IsReadOnly)
//             {
//                 continue;
//             }
//
//             var baseTypeName = p.Type.BaseType?.Name;
//             var isValueOf = baseTypeName is "ValueOf";
//             var valueOfArgument = p.Type.BaseType?.TypeArguments.FirstOrDefault()?.ToString() ?? "";
//
//             sb.Append($"{p.Name} = ");
//
//             if (isValueOf)
//             {
//                 sb.Append($"{p.Type}.From(");
//             }
//
//             var realType = isValueOf
//                 ? valueOfArgument
//                 : p.Type.ToString();
//
//             switch (realType)
//             {
//                 case "System.Guid":
//                     sb.Append($"Guid.Parse({st.VarModelDto}.{p.Name})");
//                     break;
//                 case "System.DateOnly":
//                     sb.Append($"DateOnly.FromDateTime({st.VarModelDto}.{p.Name})");
//                     break;
//                 default:
//                     sb.Append($"{st.VarModelDto}.{p.Name}");
//                     break;
//             }
//
//             if (isValueOf)
//             {
//                 sb.Append(")");
//             }
//
//             if (!isLastItem)
//             {
//                 sb.AppendLine(",");
//             }
//         }
//
//         return $$"""
//
//                 public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassModelDto}} {{st.VarModelDto}})
//                 {
//                     return new {{st.Model}}
//                     {
//                         {{sb}}
//                     };
//                 }
//             """;
//     }
// }
