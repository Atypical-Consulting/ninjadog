// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// This template generates the Update request for a given entity.
/// </summary>
public sealed class UpdateRequestTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Requests";

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
