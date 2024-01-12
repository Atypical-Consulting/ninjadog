// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;

public sealed class DtoTemplate : NinjadogTemplate
{
    public override IEnumerable<string> GenerateOneToMany(NinjadogSettings ninjadogSettings)
    {
        var entities = ninjadogSettings.Entities.FromKeys();
        var rootNs = ninjadogSettings.Config.RootNamespace;

        foreach (var entity in entities)
        {
            yield return GenerateDto(entity, rootNs);
        }
    }

    private static string GenerateDto(NinjadogEntityWithKey entity, string rootNs)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNs}.Contracts.Data";

        return DefaultCodeLayout(
            $$"""

              using System.Collections.Generic;
              using {{rootNs}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelDto}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """);
    }
}
