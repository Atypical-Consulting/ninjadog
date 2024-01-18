// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

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

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}}.Domain;
              using {{rootNamespace}}.Mapping;
              using {{rootNamespace}}.Repositories;
              using FluentValidation;
              using FluentValidation.Results;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelService}} : {{st.InterfaceModelService}}
              {
                  private readonly {{st.InterfaceModelRepository}} {{st.FieldModelRepository}};

                  public {{st.ClassModelService}}({{st.InterfaceModelRepository}} {{st.VarModelRepository}})
                  {
                      {{st.FieldModelRepository}} = {{st.VarModelRepository}};
                  }

                  public async Task<bool> CreateAsync({{st.Model}} {{st.VarModel}})
                  {
                      // TODO: rename existingUser variable
                      var {{st.VarExistingModel}} = await {{st.FieldModelRepository}}.GetAsync({{st.VarModel}}.Id);
                      if ({{st.VarExistingModel}} is not null)
                      {
                          var message = $"A {{st.ModelHumanized}} with id {{{st.VarModel}}.Id} already exists";
                          throw new ValidationException(message, new []
                          {
                              new ValidationFailure(nameof({{st.Model}}), message)
                          });
                      }

                      var {{st.VarModelDto}} = {{st.VarModel}}.{{st.MethodToModelDto}}();
                      return await {{st.FieldModelRepository}}.CreateAsync({{st.VarModelDto}});
                  }

                  public async Task<{{st.Model}}?> GetAsync(Guid id)
                  {
                      var {{st.VarModelDto}} = await {{st.FieldModelRepository}}.GetAsync(id);
                      return {{st.VarModelDto}}?.{{st.MethodToModel}}();
                  }

                  public async Task<IEnumerable<{{st.Model}}>> GetAllAsync()
                  {
                      var {{st.VarModelDtos}} = await {{st.FieldModelRepository}}.GetAllAsync();
                      return {{st.VarModelDtos}}.Select(x => x.{{st.MethodToModel}}());
                  }

                  public async Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}})
                  {
                      var {{st.VarModelDto}} = {{st.VarModel}}.{{st.MethodToModelDto}}();
                      return await {{st.FieldModelRepository}}.UpdateAsync({{st.VarModelDto}});
                  }

                  public async Task<bool> DeleteAsync(Guid id)
                  {
                      return await {{st.FieldModelRepository}}.DeleteAsync(id);
                  }
              }
              """);
    }
}
