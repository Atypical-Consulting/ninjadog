// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// This template generates the repository interface for a given entity.
/// </summary>
public sealed class RepositoryInterfaceTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "RepositoryInterface";

    /// <inheritdoc/>
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Repositories";
        var fileName = $"{st.InterfaceModelRepository}.cs";
        var entityKey = entity.Properties.GetEntityKey();

        var content =
            $$"""

              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial interface {{st.InterfaceModelRepository}}
              {
                  Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}});

                  Task<{{st.ClassModelDto}}?> GetAsync({{entityKey.Type}} id);

                  Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync();

                  Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}});

                  Task<bool> DeleteAsync(Guid id);
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
