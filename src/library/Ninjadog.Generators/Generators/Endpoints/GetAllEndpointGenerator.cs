namespace Ninjadog.Endpoints;

[Generator]
public sealed class GetAllEndpointGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            st => $"GetAll{st.Models}Endpoint",
            GenerateCode,
            "Endpoints");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""
            
            using {{rootNs}}.Contracts.Responses;
            using {{rootNs}}.Mapping;
            using {{rootNs}}.Services;
            using FastEndpoints;
            using Microsoft.AspNetCore.Authorization;
            
            {{WriteFileScopedNamespace(ns)}}
            
            public partial class {{st.ClassGetAllModelsEndpoint}}
                : EndpointWithoutRequest<{{st.ClassGetAllModelsResponse}}>
            {
                public {{st.InterfaceModelService}} {{st.PropertyModelService}} { get; private set; } = null!;
            
                public override void Configure()
                {
                    Get("{{st.ModelEndpoint}}");
                    AllowAnonymous();
                }
            
                public override async Task HandleAsync(CancellationToken ct)
                {
                    var {{st.VarModels}} = await {{st.PropertyModelService}}.GetAllAsync();
                    var {{st.VarModelsResponse}} = {{st.VarModels}}.{{st.MethodToModelsResponse}}();
                    await SendOkAsync({{st.VarModelsResponse}}, ct);
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
