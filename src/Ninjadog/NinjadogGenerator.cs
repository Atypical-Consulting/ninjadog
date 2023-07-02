namespace Ninjadog;

[Generator]
public sealed class NinjadogGenerator : NinjadogBaseGenerator
{
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            "NinjadogExtensions",
            GenerateCode);

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using Microsoft.AspNetCore.Diagnostics;
            using {{rootNs}}.Contracts.Responses;
            using {{rootNs}}.Database;
            using {{rootNs}}.Mapping;
            using {{rootNs}}.Repositories;
            using {{rootNs}}.Services;
            using {{rootNs}}.Validation;
            using FastEndpoints;
            using FastEndpoints.ClientGen;
            using FastEndpoints.Swagger;
            using FluentValidation;

            {{WriteFileScopedNamespace(rootNs)}}

            public static class NinjadogExtensions
            {
                public static IServiceCollection AddNinjadog(
                    this IServiceCollection services,
                    ConfigurationManager config)
                {
                    services.AddFastEndpoints();
                    services.SwaggerDocument(o =>
                    {
                        o.DocumentSettings = s =>
                        {
                            s.Title = "Ninjadog API";
                            s.Version = "v1";
                        };
                    });

                    var connectionString =
                        config.GetValue<string>("Database:ConnectionString")
                        ?? throw new InvalidOperationException("Database:ConnectionString is not configured.");

                    services.AddSingleton<IDbConnectionFactory>(_ => new SqliteConnectionFactory(connectionString));
                    services.AddSingleton<DatabaseInitializer>();

            {{GenerateModelDependenciesInjection(typeContexts)}}
                    return services;
                }

                public static WebApplication UseNinjadog(this WebApplication app)
                {
                    app.UseValidationExceptionHandler();
                    app.UseFastEndpoints();
                    app.UseSwaggerGen();

                    app.MapCSharpClientEndpoint("/cs-client", "version 1", s =>
                    {
                        s.ClassName = "ApiClient";
                        s.CSharpGeneratorSettings.Namespace = "{{rootNs}}.Client";
                    });

                    app.MapTypeScriptClientEndpoint("/ts-client", "version 1", s =>
                    {
                        s.ClassName = "ApiClient";
                        s.TypeScriptGeneratorSettings.Namespace = "{{rootNs}}.Client";
                    });

                    return app;
                }

                public static WebApplication UseValidationExceptionHandler(this WebApplication app)
                {
                    app.UseExceptionHandler(errApp =>
                    {
                        errApp.Run(async ctx =>
                        {
                            var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                            if (exHandlerFeature?.Error is ValidationException exception)
                            {
                                var validationFailureResponse = new ErrorResponse
                                {
                                    StatusCode = 400,
                                    Message = "One or more errors occured!",
                                    Errors = exception.Errors
                                        .GroupBy(failure => failure.PropertyName)
                                        .ToDictionary(
                                            failures => failures.Key,
                                            failures => failures.Select(failure => failure.ErrorMessage).ToList())
                                };

                                await ctx.Response.WriteAsJsonAsync(validationFailureResponse);
                            }
                        });
                    });

                    return app;
                }
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateModelDependenciesInjection(ImmutableArray<TypeContext> typeContexts)
    {
        IndentedStringBuilder sb = new();
        sb.Indent();
        sb.Indent();

        foreach (var st in typeContexts.Select(model => model.Tokens))
        {
            sb.AppendLine($"services.AddSingleton<{st.InterfaceModelRepository}, {st.ClassModelRepository}>();");
            sb.AppendLine($"services.AddSingleton<{st.InterfaceModelService}, {st.ClassModelService}>();");
        }

        return sb.ToString();
    }
}
