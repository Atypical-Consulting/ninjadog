// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// This template generates the Update request for a given entity.
/// </summary>
public sealed class UpdateRequestTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "UpdateRequest";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Requests";
        var fileName = $"{st.ClassUpdateModelRequest}.cs";

        var content =
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              /// <summary>
              ///     Request to update a {{st.Model}}.
              /// </summary>
              public partial class {{st.ClassUpdateModelRequest}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
