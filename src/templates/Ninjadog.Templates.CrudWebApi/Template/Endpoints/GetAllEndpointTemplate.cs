// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Endpoints;

/// <summary>
/// This template generates the GetAll endpoint for a given entity.
/// </summary>
public sealed class GetAllEndpointTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "GetAllEndpoint";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Endpoints";
        var fileName = Path.Combine(st.Model, $"{st.ClassGetAllModelsEndpoint}.cs");

        var content =
            $$"""

              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Mapping;
              using {{rootNamespace}}.Services;
              using FastEndpoints;
              using Microsoft.AspNetCore.Authorization;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassGetAllModelsEndpoint}}
                  : EndpointWithoutRequest<{{st.ClassGetAllModelsResponse}}>
              {
                  public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;

                  public override void Configure()
                  {
                      Get("{{st.ModelEndpoint}}");
                      AllowAnonymous();
                  }

                  public override async Task HandleAsync(CancellationToken ct)
                  {
                      var {{st.VarModels}} = await {{st.PropertyModelService}}.GetAllAsync();
                      var {{st.VarModelsResponse}} = {{st.VarModels}}.{{st.MethodToModelsResponse}}();
                      await SendOkAsync({{st.VarModelsResponse}}, ct);
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
