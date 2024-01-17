// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Templates.CrudWebAPI.Template.Endpoints;

/// <summary>
/// This template generates the Update endpoint for a given entity.
/// </summary>
public sealed class UpdateEndpointTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "UpdateEndpoint";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Endpoints";
        var fileName = $"{st.ClassUpdateModelEndpoint}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}}.Contracts.Requests;
              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Mapping;
              using {{rootNamespace}}.Services;
              using FastEndpoints;
              using Microsoft.AspNetCore.Authorization;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassUpdateModelEndpoint}}
                  : Endpoint<{{st.ClassUpdateModelRequest}}, {{st.ClassModelResponse}}>
              {
                  public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;

                  public override void Configure()
                  {
                      Put("{{st.ModelEndpoint}}/{id:guid}");
                      AllowAnonymous();
                  }

                  public override async Task HandleAsync({{st.ClassUpdateModelRequest}} req, CancellationToken ct)
                  {
                      var {{st.VarExistingModel}} = await {{st.PropertyModelService}}.GetAsync(req.Id);

                      if ({{st.VarExistingModel}} is null)
                      {
                          await SendNotFoundAsync(ct);
                          return;
                      }

                      var {{st.VarModel}} = req.{{st.MethodToModel}}();
                      await {{st.PropertyModelService}}.UpdateAsync({{st.VarModel}});

                      var {{st.VarModelResponse}} = {{st.VarModel}}.{{st.MethodToModelResponse}}();
                      await SendOkAsync({{st.VarModelResponse}}, ct);
                  }
              }
              """);
    }
}
