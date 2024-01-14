// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Summaries;

public sealed class DeleteSummaryTemplate
    : NinjadogTemplate
{
    public override string GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Summaries";

        return DefaultCodeLayout(
            $$"""

              using {{rootNamespace}}.Endpoints;
              using FastEndpoints;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassDeleteModelSummary}} : Summary<{{st.ClassDeleteModelEndpoint}}>
              {
                  public {{st.ClassDeleteModelSummary}}()
                  {
                      Summary = "Delete a {{st.ModelHumanized}} in the system";
                      Description = "Delete a {{st.ModelHumanized}} in the system";
                      Response(204, "The {{st.ModelHumanized}} was deleted successfully");
                      Response(404, "The {{st.ModelHumanized}} was not found in the system");
                  }
              }
              """);
    }
}
