// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Summaries;

[Generator]
public sealed class CreateSummaryGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Create{st.Model}Summary",
            GenerateCode,
            "Summaries");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Contracts.Responses;
            using {{rootNs}}.Endpoints;
            using FastEndpoints;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassCreateModelSummary}} : Summary<{{st.ClassCreateModelEndpoint}}>
            {
                public {{st.ClassCreateModelSummary}}()
                {
                    Summary = "Creates a new {{st.ModelHumanized}} in the system";
                    Description = "Creates a new {{st.ModelHumanized}} in the system";
                    Response<{{st.ClassModelResponse}}>(201, "{{st.ModelHumanized}} was successfully created");
                    Response<ErrorResponse>(400, "The request did not pass validation checks");
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
