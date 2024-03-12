// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Endpoints;

/// <summary>
/// This template generates the Get endpoint for a given entity.
/// </summary>
public sealed class GetEndpointTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "GetEndpoint";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Endpoints";
        var fileName = Path.Combine(st.Model, $"{st.ClassGetModelEndpoint}.cs");

        var content =
            $$"""

              using {{rootNamespace}}.Contracts.Requests;
              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Mapping;
              using {{rootNamespace}}.Services;
              using FastEndpoints;
              using Microsoft.AspNetCore.Authorization;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassGetModelEndpoint}}
                  : Endpoint<{{st.ClassGetModelRequest}}, {{st.ClassModelResponse}}>
              {
                  public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;

                  public override void Configure()
                  {
                      Get("{{st.ModelEndpoint}}/{id:guid}");
                      AllowAnonymous();
                  }

                  public override async Task HandleAsync({{st.ClassGetModelRequest}} req, CancellationToken ct)
                  {
                      var {{st.VarModel}} = await {{st.PropertyModelService}}.GetAsync(req.Id);

                      if ({{st.VarModel}} is null)
                      {
                          await SendNotFoundAsync(ct);
                          return;
                      }

                      var {{st.VarModelResponse}} = {{st.VarModel}}.{{st.MethodToModelResponse}}();
                      await SendOkAsync({{st.VarModelResponse}}, ct);
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
