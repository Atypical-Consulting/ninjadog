using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Database;

[Generator]
public sealed class DatabaseInitializerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
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
        var code = GenerateCode(type);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Database";

        const string className = "DatabaseInitializer";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Database" : null;

        var code = @$"
using Dapper;

{Utilities.WriteFileScopedNamespace(ns)}

public partial class DatabaseInitializer
{{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {{
        _connectionFactory = connectionFactory;
    }}

    public async Task InitializeAsync()
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        // TODO: Generate SQL query for database creation
        // await connection.ExecuteAsync(@""CREATE TABLE IF NOT EXISTS Customers (
        // Id CHAR(36) PRIMARY KEY,
        // Username TEXT NOT NULL,
        // FullName TEXT NOT NULL,
        // Email TEXT NOT NULL,
        // DateOfBirth TEXT NOT NULL)"");
    }}
}}";

        return Utilities.DefaultCodeLayout(code);
    }
}
