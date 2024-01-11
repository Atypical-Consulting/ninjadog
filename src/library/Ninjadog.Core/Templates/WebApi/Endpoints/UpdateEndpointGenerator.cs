// namespace Ninjadog.Endpoints;
//
// [Generator]
// public sealed class UpdateEndpointGenerator : NinjadogIncrementalGeneratorBase
// {
//     /// <inheritdoc />
//     protected override IncrementalGeneratorSetup Setup
//         => new(
//             st => $"Update{st.Model}Endpoint",
//             GenerateCode,
//             "Endpoints");
//
//     private static string GenerateCode(TypeContext typeContext)
//     {
//         var (st, ns) = typeContext;
//         var rootNs = typeContext.RootNamespace;
//
//         var code = $$"""
//
//             using {{rootNs}}.Contracts.Requests;
//             using {{rootNs}}.Contracts.Responses;
//             using {{rootNs}}.Mapping;
//             using {{rootNs}}.Services;
//             using FastEndpoints;
//             using Microsoft.AspNetCore.Authorization;
//
//             {{WriteFileScopedNamespace(ns)}}
//
//             public partial class {{st.ClassUpdateModelEndpoint}}
//                 : Endpoint<{{st.ClassUpdateModelRequest}}, {{st.ClassModelResponse}}>
//             {
//                 public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;
//
//                 public override void Configure()
//                 {
//                     Put("{{st.ModelEndpoint}}/{id:guid}");
//                     AllowAnonymous();
//                 }
//
//                 public override async Task HandleAsync({{st.ClassUpdateModelRequest}} req, CancellationToken ct)
//                 {
//                     var {{st.VarExistingModel}} = await {{st.PropertyModelService}}.GetAsync(req.Id);
//
//                     if ({{st.VarExistingModel}} is null)
//                     {
//                         await SendNotFoundAsync(ct);
//                         return;
//                     }
//
//                     var {{st.VarModel}} = req.{{st.MethodToModel}}();
//                     await {{st.PropertyModelService}}.UpdateAsync({{st.VarModel}});
//
//                     var {{st.VarModelResponse}} = {{st.VarModel}}.{{st.MethodToModelResponse}}();
//                     await SendOkAsync({{st.VarModelResponse}}, ct);
//                 }
//             }
//             """;
//
//         return DefaultCodeLayout(code);
//     }
// }
