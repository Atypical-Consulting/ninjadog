// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Summaries;

/// <summary>
/// This template generates the summary for the get endpoint of a given entity.
/// </summary>
public sealed class GetSummaryTemplate
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

              public partial class {{st.ClassGetModelSummary}} : Summary<{{st.ClassGetModelEndpoint}}>
              {
                  public {{st.ClassGetModelSummary}}()
                  {
                      Summary = "Returns a single {{st.ModelHumanized}} by id";
                      Description = "Returns a single {{st.ModelHumanized}} by id";
                      Response<{{st.ClassModelResponse}}>(200, "Successfully found and returned the {{st.ModelHumanized}}");
                      Response(404, "The {{st.ModelHumanized}} does not exist in the system");
                  }
              }
              """);
    }
}
