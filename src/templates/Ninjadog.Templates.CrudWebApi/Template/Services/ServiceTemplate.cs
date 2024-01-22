// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Services;

/// <summary>
/// This template generates the service for a given entity.
/// </summary>
public sealed class ServiceTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "Service";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Services";
        var fileName = $"{st.ClassModelService}.cs";
        var entityKey = entity.Properties.GetEntityKey();

        var content =
            $$"""

              using {{rootNamespace}}.Domain;
              using {{rootNamespace}}.Mapping;
              using {{rootNamespace}}.Repositories;
              using FluentValidation;
              using FluentValidation.Results;

              {{WriteFileScopedNamespace(ns)}}

              /// <summary>
              /// This service provides CRUD operations for {{st.ModelsHumanized}}.
              /// </summary>
              /// <param name="{{st.VarModelRepository}}">The repository for {{st.ModelsHumanized}}.</param>
              public partial class {{st.ClassModelService}}({{st.InterfaceModelRepository}} {{st.VarModelRepository}})
                  : {{st.InterfaceModelService}}
              {
                  /// <summary>
                  /// Asynchronously creates a new {{st.ModelHumanized}}.
                  /// </summary>
                  /// <param name="{{st.VarModel}}">The {{st.ModelHumanized}} to create.</param>
                  /// <returns>True if the creation was successful, false otherwise.</returns>
                  /// <exception cref="ValidationException">Thrown if a {{st.ModelHumanized}} with the same ID already exists.</exception>
                  public async Task<bool> CreateAsync({{st.Model}} {{st.VarModel}})
                  {
                      var {{st.VarExistingModel}} = await {{st.VarModelRepository}}.GetAsync({{st.VarModel}}.{{entityKey.Key}});
                      if ({{st.VarExistingModel}} is not null)
                      {
                          var message = $"A {{st.ModelHumanized}} with id {{{st.VarModel}}.{{entityKey.Key}}} already exists";
                          throw new ValidationException(message, new []
                          {
                              new ValidationFailure(nameof({{st.Model}}), message)
                          });
                      }

                      var {{st.VarModelDto}} = {{st.VarModel}}.{{st.MethodToModelDto}}();
                      return await {{st.VarModelRepository}}.CreateAsync({{st.VarModelDto}});
                  }

                  /// <summary>
                  /// Retrieves a {{st.ModelHumanized}} by its ID.
                  /// </summary>
                  /// <param name="id">The ID of the {{st.ModelHumanized}} to retrieve.</param>
                  /// <returns>The requested {{st.ModelHumanized}}, or null if not found.</returns>
                  public async Task<{{st.Model}}?> GetAsync(Guid id)
                  {
                      var {{st.VarModelDto}} = await {{st.VarModelRepository}}.GetAsync(id);
                      return {{st.VarModelDto}}?.{{st.MethodToModel}}();
                  }

                  /// <summary>
                  /// Retrieves all {{st.ModelsHumanized}}.
                  /// </summary>
                  /// <returns>A collection of all {{st.ModelsHumanized}}.</returns>
                  public async Task<IEnumerable<{{st.Model}}>> GetAllAsync()
                  {
                      var {{st.VarModelDtos}} = await {{st.VarModelRepository}}.GetAllAsync();
                      return {{st.VarModelDtos}}.Select(x => x.{{st.MethodToModel}}());
                  }

                  /// <summary>
                  /// Updates an existing {{st.ModelHumanized}}.
                  /// </summary>
                  /// <param name="{{st.VarModel}}">The {{st.ModelHumanized}} to update.</param>
                  /// <returns>True if the update was successful, false otherwise.</returns>
                  public async Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}})
                  {
                      var {{st.VarModelDto}} = {{st.VarModel}}.{{st.MethodToModelDto}}();
                      return await {{st.VarModelRepository}}.UpdateAsync({{st.VarModelDto}});
                  }

                  /// <summary>
                  /// Deletes a {{st.ModelHumanized}} by its ID.
                  /// </summary>
                  /// <param name="id">The ID of the {{st.ModelHumanized}} to delete.</param>
                  /// <returns>True if the deletion was successful, false otherwise.</returns>
                  public async Task<bool> DeleteAsync(Guid id)
                  {
                      return await {{st.VarModelRepository}}.DeleteAsync(id);
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
