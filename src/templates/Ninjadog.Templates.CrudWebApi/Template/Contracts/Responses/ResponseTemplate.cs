// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Responses;

/// <summary>
/// This template generates the response for a given entity.
/// </summary>
public sealed class ResponseTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Responses";
        var fileName = $"{st.ClassModelResponse}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelResponse}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """);
    }
}
