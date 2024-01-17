// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;
using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Validation;

/// <summary>
/// This template generates the validation rules for the update request of a given entity.
/// </summary>
public sealed class UpdateRequestValidatorTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "UpdateRequestValidator";

    /// <inheritdoc/>
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Validation";
        var fileName = $"{st.ClassUpdateModelRequestValidator}.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}}.Contracts.Requests;
              using FastEndpoints;
              using FluentValidation;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassUpdateModelRequestValidator}} : Validator<{{st.ClassUpdateModelRequest}}>
              {
                  public {{st.ClassUpdateModelRequestValidator}}()
                  {
                      {{GenerateValidationRules(entity)}}
                  }
              }
              """);
    }

    private static string GenerateValidationRules(NinjadogEntityWithKey entity)
    {
        var properties = entity.Properties;

        IndentedStringBuilder sb = new(2);

        foreach (var (propertyName, propertyValue) in properties)
        {
            if (propertyValue.IsKey)
            {
                continue;
            }

            // Applying validation rules for each property
            sb.AppendLine($"RuleFor(x => x.{propertyName}).NotEmpty();");

            // Check if not the last property to add an extra line break
            if (propertyName != properties.Last().Key)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
