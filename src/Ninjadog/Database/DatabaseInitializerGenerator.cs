using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Database;

[Generator]
public sealed class DatabaseInitializerGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        var type = models[0];
        const string className = "DatabaseInitializer";

        context.AddSource(
            $"{GetRootNamespace(type)}.Database.{className}.g.cs",
            GenerateCode(type));
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Database" : null;

        var code = @$"
using Dapper;

{WriteFileScopedNamespace(ns)}

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

        return DefaultCodeLayout(code);
    }
}
