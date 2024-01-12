// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Services;

[Generator]
public sealed class ServiceInterfaceGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"I{st.Model}Service",
            GenerateCode,
            "Services");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Domain;

            {{WriteFileScopedNamespace(ns)}}

            public partial interface {{st.InterfaceModelService}}
            {
                Task<bool> CreateAsync({{st.Model}} {{st.VarModel}});

                Task<{{st.Model}}?> GetAsync(Guid id);

                Task<IEnumerable<{{st.Model}}>> GetAllAsync();

                Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}});

                Task<bool> DeleteAsync(Guid id);
            }
            """;

        return DefaultCodeLayout(code);
    }
}
