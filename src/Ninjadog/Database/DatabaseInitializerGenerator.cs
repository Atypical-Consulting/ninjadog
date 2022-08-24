namespace Ninjadog.Database;

[Generator]
public sealed class DatabaseInitializerGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            "DatabaseInitializer",
            GenerateCode,
            "Database");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;

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
