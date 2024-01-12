// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Contracts.Responses;

[Generator]
public sealed class GetAllResponseGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"GetAll{st.Models}Response",
            GenerateCode,
            "Contracts.Responses");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;

        var code = $$"""

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassGetAllModelsResponse}}
            {
                public IEnumerable<{{st.ClassModelResponse}}> {{st.Models}} { get; init; } = Enumerable.Empty<{{st.ClassModelResponse}}>();
            }
            """;

        return DefaultCodeLayout(code);
    }
}
