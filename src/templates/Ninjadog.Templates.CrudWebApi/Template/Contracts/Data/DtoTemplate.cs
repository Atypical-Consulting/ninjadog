// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;

public sealed class DtoTemplate
    : NinjadogTemplate
{
    public override string GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Data";

        return DefaultCodeLayout(
            $$"""

              using System.Collections.Generic;
              using {{rootNamespace}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelDto}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """);
    }
}
