// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Responses;

public sealed class GetAllResponseTemplate
    : NinjadogTemplate
{
    public override IEnumerable<string> GenerateOneToMany(NinjadogSettings ninjadogSettings)
    {
        var entities = ninjadogSettings.Entities.FromKeys();
        var rootNs = ninjadogSettings.Config.RootNamespace;

        foreach (var entity in entities)
        {
            yield return GenerateGetAllResponse(entity, rootNs);
        }
    }

    private static string GenerateGetAllResponse(NinjadogEntityWithKey entity, string rootNs)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNs}.Contracts.Responses";

        return DefaultCodeLayout(
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassGetAllModelsResponse}}
              {
                  public IEnumerable<{{st.ClassModelResponse}}> {{st.Models}} { get; init; } = Enumerable.Empty<{{st.ClassModelResponse}}>();
              }
              """);
    }
}
