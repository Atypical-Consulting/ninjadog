// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

public sealed class UpdateRequestTemplate
    : NinjadogTemplate
{
    public override IEnumerable<string> GenerateOneToMany(NinjadogSettings ninjadogSettings)
    {
        var entities = ninjadogSettings.Entities.FromKeys();
        var rootNs = ninjadogSettings.Config.RootNamespace;

        foreach (var entity in entities)
        {
            yield return GenerateUpdateRequest(entity, rootNs);
        }
    }

    private static string GenerateUpdateRequest(NinjadogEntityWithKey entity, string rootNs)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNs}.Contracts.Requests";

        return DefaultCodeLayout(
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              /// <summary>
              ///     Request to update a {{st.Model}}.
              /// </summary>
              public partial class {{st.ClassUpdateModelRequest}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """);
    }
}
