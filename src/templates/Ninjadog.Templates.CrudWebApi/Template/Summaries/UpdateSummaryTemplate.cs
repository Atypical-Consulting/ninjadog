// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Templates.CrudWebAPI.Template.Summaries;

/// <summary>
/// This template generates the summary for the update endpoint of a given entity.
/// </summary>
public sealed class UpdateSummaryTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "UpdateSummary";

    /// <inheritdoc/>
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Summaries";
        var fileName = $"{st.ClassUpdateModelSummary}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Endpoints;
              using FastEndpoints;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassUpdateModelSummary}} : Summary<{{st.ClassUpdateModelEndpoint}}>
              {
                  public {{st.ClassUpdateModelSummary}}()
                  {
                      Summary = "Updates an existing {{st.ModelHumanized}} in the system";
                      Description = "Updates an existing {{st.ModelHumanized}} in the system";
                      Response<{{st.ClassModelResponse}}>(201, "{{st.ModelHumanized}} was successfully updated");
                      Response<ErrorResponse>(400, "The request did not pass validation checks");
                  }
              }
              """);
    }
}
