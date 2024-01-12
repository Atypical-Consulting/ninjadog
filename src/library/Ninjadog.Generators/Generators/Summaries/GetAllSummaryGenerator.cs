// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Summaries;

[Generator]
public sealed class GetAllSummaryGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"GetAll{st.Models}Summary",
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

            public partial class {{st.ClassGetAllModelsSummary}} : Summary<{{st.ClassGetAllModelsEndpoint}}>
            {
                public {{st.ClassGetAllModelsSummary}}()
                {
                    Summary = "Returns all the {{st.ModelsHumanized}} in the system";
                    Description = "Returns all the {{st.ModelsHumanized}} in the system";
                    Response<{{st.ClassGetAllModelsResponse}}>(200, "All {{st.ModelsHumanized}} in the system are returned");
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
