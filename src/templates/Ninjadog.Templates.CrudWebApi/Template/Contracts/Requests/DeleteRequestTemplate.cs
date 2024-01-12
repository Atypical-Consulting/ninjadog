// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

public sealed class DeleteRequestTemplate
    : NinjadogTemplate
{
    public override string GenerateOneToManyForEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Requests";

        return DefaultCodeLayout(
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              /// <summary>
              ///     Request to delete a {{st.Model}}.
              /// </summary>
              public partial class {{st.ClassDeleteModelRequest}}
              {
                  public Guid Id { get; init; }
              }
              """);
    }
}
