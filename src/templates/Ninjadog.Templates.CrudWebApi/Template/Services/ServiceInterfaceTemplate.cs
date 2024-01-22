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
        var entityKey = entity.Properties.GetEntityKey();

        var content =
            $$"""

              using {{rootNamespace}}.Domain;

              {{WriteFileScopedNamespace(ns)}}

              public partial interface {{st.InterfaceModelService}}
              {
                  /// <summary>
                  /// Asynchronously creates a new {{st.ModelHumanized}}.
                  /// </summary>
                  /// <param name="{{st.VarModel}}">The {{st.ModelHumanized}} to create.</param>
                  /// <returns>True if the creation was successful, false otherwise.</returns>
                  /// <exception cref="ValidationException">Thrown if a {{st.ModelHumanized}} with the same ID already exists.</exception>
                  Task<bool> CreateAsync({{st.Model}} {{st.VarModel}});

                  /// <summary>
                  /// Retrieves a {{st.ModelHumanized}} by its ID.
                  /// </summary>
                  /// <param name="id">The ID of the {{st.ModelHumanized}} to retrieve.</param>
                  /// <returns>The requested {{st.ModelHumanized}}, or null if not found.</returns>
                  Task<{{st.Model}}?> GetAsync({{entityKey.Type}} id);

                  /// <summary>
                  /// Retrieves all {{st.ModelsHumanized}}.
                  /// </summary>
                  /// <returns>A collection of all {{st.ModelsHumanized}}.</returns>
                  Task<IEnumerable<{{st.Model}}>> GetAllAsync();

                  /// <summary>
                  /// Updates an existing {{st.ModelHumanized}}.
                  /// </summary>
                  /// <param name="{{st.VarModel}}">The {{st.ModelHumanized}} to update.</param>
                  /// <returns>True if the update was successful, false otherwise.</returns>
                  Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}});

                  /// <summary>
                  /// Deletes a {{st.ModelHumanized}} by its ID.
                  /// </summary>
                  /// <param name="id">The ID of the {{st.ModelHumanized}} to delete.</param>
                  /// <returns>True if the deletion was successful, false otherwise.</returns>
                  Task<bool> DeleteAsync({{entityKey.Type}} id);
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
