// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Summaries;

/// <summary>
/// This template generates the summary for the create endpoint of a given entity.
/// </summary>
public sealed class CreateSummaryTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Summaries";

        return DefaultCodeLayout(
            $$"""

              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Endpoints;
              using FastEndpoints;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassCreateModelSummary}} : Summary<{{st.ClassCreateModelEndpoint}}>
              {
                  public {{st.ClassCreateModelSummary}}()
                  {
                      Summary = "Creates a new {{st.ModelHumanized}} in the system";
                      Description = "Creates a new {{st.ModelHumanized}} in the system";
                      Response<{{st.ClassModelResponse}}>(201, "{{st.ModelHumanized}} was successfully created");
                      Response<ErrorResponse>(400, "The request did not pass validation checks");
                  }
              }
              """);
    }
}
