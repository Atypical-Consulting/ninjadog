// namespace Ninjadog.Summaries;
//
// [Generator]
// public sealed class GetSummaryGenerator : NinjadogIncrementalGeneratorBase
// {
//     /// <inheritdoc />
//     protected override IncrementalGeneratorSetup Setup
//         => new(
//             st => $"Get{st.Model}Summary",
//             GenerateCode,
//             "Summaries");
//
//     private static string GenerateCode(TypeContext typeContext)
//     {
//         var (st, ns) = typeContext;
//         var rootNs = typeContext.RootNamespace;
//
//         var code = $$"""
//
//             using {{rootNs}}.Contracts.Responses;
//             using {{rootNs}}.Endpoints;
//             using FastEndpoints;
//
//             {{WriteFileScopedNamespace(ns)}}
//
//             public partial class {{st.ClassGetModelSummary}} : Summary<{{st.ClassGetModelEndpoint}}>
//             {
//                 public {{st.ClassGetModelSummary}}()
//                 {
//                     Summary = "Returns a single {{st.ModelHumanized}} by id";
//                     Description = "Returns a single {{st.ModelHumanized}} by id";
//                     Response<{{st.ClassModelResponse}}>(200, "Successfully found and returned the {{st.ModelHumanized}}");
//                     Response(404, "The {{st.ModelHumanized}} does not exist in the system");
//                 }
//             }
//             """;
//
//         return DefaultCodeLayout(code);
//     }
// }
