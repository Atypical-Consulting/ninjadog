// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Repositories;

[Generator]
public sealed class RepositoryInterfaceGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"I{st.Model}Repository",
            GenerateCode,
            "Repositories");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Contracts.Data;
            using {{rootNs}}.Database;
            using Dapper;

            {{WriteFileScopedNamespace(ns)}}

            public partial interface {{st.InterfaceModelRepository}}
            {
                Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}});

                Task<{{st.ClassModelDto}}?> GetAsync(Guid id);

                Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync();

                Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}});

                Task<bool> DeleteAsync(Guid id);
            }
            """;

        return DefaultCodeLayout(code);
    }
}
