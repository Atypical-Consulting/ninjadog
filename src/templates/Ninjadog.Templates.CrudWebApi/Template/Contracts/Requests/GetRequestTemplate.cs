// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

/// <summary>
/// This template generates the Get request for a given entity.
/// </summary>
public sealed class GetRequestTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "GetRequest";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Requests";
        var fileName = $"{st.ClassGetModelRequest}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              /// <summary>
              ///     Request to get a {{st.Model}}.
              /// </summary>
              public partial class {{st.ClassGetModelRequest}}
              {
                  public Guid Id { get; init; }
              }
              """);
    }
}
