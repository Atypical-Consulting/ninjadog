// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Validation;

[Generator]
public sealed class UpdateRequestValidatorGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Update{st.Model}RequestValidator",
            GenerateCode,
            "Validation");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Contracts.Requests;
            using FastEndpoints;
            using FluentValidation;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassUpdateModelRequestValidator}} : Validator<{{st.ClassUpdateModelRequest}}>
            {
                public {{st.ClassUpdateModelRequestValidator}}()
                {
                    {{GenerateValidationRules(typeContext)}}
                }
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateValidationRules(TypeContext typeContext)
    {
        var properties = typeContext.PropertyContexts;

        IndentedStringBuilder sb = new(2);

        foreach (var context in properties.Where(context => !context.IsId))
        {
            if (!context.IsLast)
            {
                sb.AppendLine($"RuleFor(x => x.{context.Name}).NotEmpty();");
            }
            else
            {
                sb.Append($"RuleFor(x => x.{context.Name}).NotEmpty();");
            }
        }

        return sb.ToString();
    }
}
