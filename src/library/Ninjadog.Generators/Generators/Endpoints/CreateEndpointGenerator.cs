// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Endpoints;

[Generator]
public sealed class CreateEndpointGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"Create{st.Model}Endpoint",
            GenerateCode,
            "Endpoints");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Contracts.Requests;
            using {{rootNs}}.Contracts.Responses;
            using {{rootNs}}.Mapping;
            using {{rootNs}}.Services;
            using FastEndpoints;
            using Microsoft.AspNetCore.Authorization;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassCreateModelEndpoint}}
                : Endpoint<{{st.ClassCreateModelRequest}}, {{st.ClassModelResponse}}>
            {
                public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;

                public override void Configure()
                {
                    Post("{{st.ModelEndpoint}}");
                    AllowAnonymous();
                }

                public override async Task HandleAsync({{st.ClassCreateModelRequest}} req, CancellationToken ct)
                {
                    var {{st.VarModel}} = req.{{st.MethodToModel}}();

                    await {{st.PropertyModelService}}.CreateAsync({{st.VarModel}});

                    var {{st.VarModelResponse}} = {{st.VarModel}}.{{st.MethodToModelResponse}}();
                    await SendCreatedAtAsync<{{st.ClassGetModelEndpoint}}>(
                        new { Id = {{st.VarModel}}.Id.Value }, {{st.VarModelResponse}}, generateAbsoluteUrl: true, cancellation: ct);
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
