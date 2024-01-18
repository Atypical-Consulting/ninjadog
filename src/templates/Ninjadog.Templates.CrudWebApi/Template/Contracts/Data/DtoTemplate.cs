// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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

        return CreateNinjadogContentFile(fileName,
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelDto}}
              {
              {{entity.GenerateMemberProperties()}}
              }
              """);
    }
}
