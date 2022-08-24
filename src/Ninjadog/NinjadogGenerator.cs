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

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Database;
using {rootNs}.Mapping;
using {rootNs}.Repositories;
using {rootNs}.Services;
using {rootNs}.Validation;
using FastEndpoints;
using FastEndpoints.Swagger;

{WriteFileScopedNamespace(rootNs)}

public static class NinjadogExtensions
{{
    public static IServiceCollection AddNinjadog(
        this IServiceCollection services,
        ConfigurationManager config)
    {{
        services.AddFastEndpoints();
        services.AddSwaggerDoc();

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new SqliteConnectionFactory(config.GetValue<string>(""Database:ConnectionString"")));
        services.AddSingleton<DatabaseInitializer>();

{GenerateModelDependenciesInjection(typeContexts)}
        return services;
    }}

    public static WebApplication UseNinjadog(this WebApplication app)
    {{
        app.UseMiddleware<ValidationExceptionMiddleware>();
        app.UseFastEndpoints(x =>
        {{
            x.ErrorResponseBuilder = (failures, _) =>
            {{
                return new ValidationFailureResponse
                {{
                    Errors = failures.Select(y => y.ErrorMessage).ToList()
                }};
            }};
        }});

        app.UseOpenApi();
        app.UseSwaggerUi3(s => s.ConfigureDefaults());

        // var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
        // await databaseInitializer.InitializeAsync();

        return app;
    }}
}}";

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
