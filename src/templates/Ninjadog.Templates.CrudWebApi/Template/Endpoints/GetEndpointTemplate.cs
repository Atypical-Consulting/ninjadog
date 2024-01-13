// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Endpoints;

public sealed class GetEndpointTemplate
    : NinjadogTemplate
{
    public override string GenerateOneToManyForEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Endpoints";

        return DefaultCodeLayout(
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
              """);
    }
}
