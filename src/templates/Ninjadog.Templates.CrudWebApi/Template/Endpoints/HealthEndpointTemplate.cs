namespace Ninjadog.Templates.CrudWebAPI.Template.Endpoints;

/// <summary>
/// This template generates the Health endpoint (/health) for liveness checks.
/// </summary>
public sealed class HealthEndpointTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "HealthEndpoint";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var ns = $"{rootNamespace}.Endpoints";
        const string fileName = "HealthEndpoint.cs";

        var content =
            $$"""

              using FastEndpoints;

              {{WriteFileScopedNamespace(ns)}}

              public sealed class HealthEndpoint : EndpointWithoutRequest
              {
                  public override void Configure()
                  {
                      Get("/health");
                      AllowAnonymous();
                  }

                  public override async Task HandleAsync(CancellationToken ct)
                  {
                      HttpContext.Response.StatusCode = 200;
                      await HttpContext.Response.WriteAsJsonAsync(new { Status = "Healthy" }, ct);
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
