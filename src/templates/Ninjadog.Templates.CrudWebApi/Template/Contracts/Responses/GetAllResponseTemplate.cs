// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Contracts.Responses;

/// <summary>
/// This template generates the GetAll response for a given entity.
/// </summary>
public sealed class GetAllResponseTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "GetAllResponse";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Contracts.Responses";
        var fileName = $"{st.ClassGetAllModelsResponse}.cs";

        var content =
            $$"""

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassGetAllModelsResponse}}
              {
                  public IEnumerable<{{st.ClassModelResponse}}> {{st.Models}} { get; init; } = Enumerable.Empty<{{st.ClassModelResponse}}>();
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
