// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Summaries;

[Generator]
public sealed class DeleteSummaryGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Delete{st.Model}Summary",
            GenerateCode,
            "Summaries");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Endpoints;
            using FastEndpoints;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassDeleteModelSummary}} : Summary<{{st.ClassDeleteModelEndpoint}}>
            {
                public {{st.ClassDeleteModelSummary}}()
                {
                    Summary = "Delete a {{st.ModelHumanized}} in the system";
                    Description = "Delete a {{st.ModelHumanized}} in the system";
                    Response(204, "The {{st.ModelHumanized}} was deleted successfully");
                    Response(404, "The {{st.ModelHumanized}} was not found in the system");
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
