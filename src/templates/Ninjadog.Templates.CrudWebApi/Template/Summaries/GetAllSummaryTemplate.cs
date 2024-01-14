// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Summaries;

/// <summary>
/// This template generates the summary for the get all endpoint of a given entity.
/// </summary>
public sealed class GetAllSummaryTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override NinjadogContentFile? GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Summaries";
        var fileName = $"{st.ClassGetAllModelsSummary}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Endpoints;
              using FastEndpoints;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassGetAllModelsSummary}} : Summary<{{st.ClassGetAllModelsEndpoint}}>
              {
                  public {{st.ClassGetAllModelsSummary}}()
                  {
                      Summary = "Returns all the {{st.ModelsHumanized}} in the system";
                      Description = "Returns all the {{st.ModelsHumanized}} in the system";
                      Response<{{st.ClassGetAllModelsResponse}}>(200, "All {{st.ModelsHumanized}} in the system are returned");
                  }
              }
              """);
    }
}
