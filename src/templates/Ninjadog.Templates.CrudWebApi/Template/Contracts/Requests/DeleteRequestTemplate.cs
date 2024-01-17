// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// This template generates the Delete request for a given entity.
/// </summary>
public sealed class DeleteRequestTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "DeleteRequest";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Requests";
        var fileName = $"{st.ClassDeleteModelRequest}.cs";

        return CreateNinjadogContentFile(fileName,
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
