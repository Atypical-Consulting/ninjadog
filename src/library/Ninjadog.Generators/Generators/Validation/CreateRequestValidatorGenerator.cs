// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Validation;

[Generator]
public sealed class CreateRequestValidatorGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Create{st.Model}RequestValidator",
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

            public partial class {{st.ClassCreateModelRequestValidator}} : Validator<{{st.ClassCreateModelRequest}}>
            {
                public {{st.ClassCreateModelRequestValidator}}()
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
            sb.AppendLine($"RuleFor(x => x.{context.Name})");
            sb.IncrementIndent();
            sb.AppendLine(".NotEmpty()");
            sb.AppendLine($".WithMessage(\"{context.Name} is required!\");");
            sb.DecrementIndent();

            if (!context.IsLast)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
