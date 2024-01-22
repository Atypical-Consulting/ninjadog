// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;

/// <summary>
/// This template generates the DTO class for a given entity.
/// </summary>
public sealed class DtoTemplate : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "DataTransferObject";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Data";
        var fileName = $"{st.ClassModelDto}.cs";

        var content =
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelDto}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
