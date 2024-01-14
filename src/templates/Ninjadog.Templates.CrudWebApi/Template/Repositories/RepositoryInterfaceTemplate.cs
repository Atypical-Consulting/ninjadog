// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// This template generates the repository interface for a given entity.
/// </summary>
public sealed class RepositoryInterfaceTemplate
    : NinjadogTemplate
{
    /// <inheritdoc/>
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Repositories";
        var fileName = $"{st.InterfaceModelRepository}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial interface {{st.InterfaceModelRepository}}
              {
                  Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}});

                  Task<{{st.ClassModelDto}}?> GetAsync(Guid id);

                  Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync();

                  Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}});

                  Task<bool> DeleteAsync(Guid id);
              }
              """);
    }
}
