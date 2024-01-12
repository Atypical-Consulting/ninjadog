// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Endpoints;

[Generator]
public sealed class DeleteEndpointGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Delete{st.Model}Endpoint",
            GenerateCode,
            "Endpoints");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Contracts.Requests;
            using {{rootNs}}.Services;
            using FastEndpoints;
            using Microsoft.AspNetCore.Authorization;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassDeleteModelEndpoint}}
                : Endpoint<{{st.ClassDeleteModelRequest}}>
            {
                public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;

                public override void Configure()
                {
                    Delete("{{st.ModelEndpoint}}/{id:guid}");
                    AllowAnonymous();
                }

                public override async Task HandleAsync({{st.ClassDeleteModelRequest}} req, CancellationToken ct)
                {
                    var deleted = await {{st.PropertyModelService}}.DeleteAsync(req.Id);
                    if (!deleted)
                    {
                        await SendNotFoundAsync(ct);
                        return;
                    }

                    await SendNoContentAsync(ct);
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
