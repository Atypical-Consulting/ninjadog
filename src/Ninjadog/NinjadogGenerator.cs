using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Ninjadog;

[Generator]
public sealed class NinjadogGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // // Add the marker attribute to the compilation
        // context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
        //     "NinjadogModelAttribute.g.cs",
        //     SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)));

        var modelTypes = Utilities.CollectNinjadogModelTypes(context);

        context.RegisterSourceOutput(modelTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        var type = models[0];
        var code = GenerateCode(models);
        var typeNamespace = Utilities.GetRootNamespace(type);

        const string className = "NinjadogExtensions";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ImmutableArray<ITypeSymbol> models)
    {
        var type = models[0];
        var rootNs = Utilities.GetRootNamespace(type);

        var code = @$"
using {rootNs}.Contracts.Responses;
using {rootNs}.Database;
using {rootNs}.Mapping;
using {rootNs}.Repositories;
using {rootNs}.Services;
using {rootNs}.Validation;
using FastEndpoints;
using FastEndpoints.Swagger;

{Utilities.WriteFileScopedNamespace(rootNs)}

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

{GenerateModelDependenciesInjection(models)}
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

        return Utilities.DefaultCodeLayout(code);
    }

    private static string GenerateModelDependenciesInjection(ImmutableArray<ITypeSymbol> models)
    {
        IndentedStringBuilder sb = new();
        sb.Indent();
        sb.Indent();

        foreach (var _ in models.Select(model => new StringTokens(model.Name)))
        {
            sb.AppendLine($"services.AddSingleton<{_.InterfaceModelRepository}, {_.ClassModelRepository}>();");
            sb.AppendLine($"services.AddSingleton<{_.InterfaceModelService}, {_.ClassModelService}>();");
        }

        return sb.ToString();
    }
}
