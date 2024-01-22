// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Services;

/// <summary>
/// This template generates the service interface for a given entity.
/// </summary>
public sealed class ServiceInterfaceTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "ServiceInterface";

    /// <inheritdoc/>
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Services";
        var fileName = $"{st.InterfaceModelService}.cs";

        var content =
            $$"""

              using {{rootNamespace}}.Domain;

              {{WriteFileScopedNamespace(ns)}}

              public partial interface {{st.InterfaceModelService}}
              {
                  Task<bool> CreateAsync({{st.Model}} {{st.VarModel}});

                  Task<{{st.Model}}?> GetAsync(Guid id);

                  Task<IEnumerable<{{st.Model}}>> GetAllAsync();

                  Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}});

                  Task<bool> DeleteAsync(Guid id);
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
