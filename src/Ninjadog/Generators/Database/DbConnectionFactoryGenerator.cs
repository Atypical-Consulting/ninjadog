namespace Ninjadog.Database;

[Generator]
public sealed class DbConnectionFactoryGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            "DbConnectionFactory",
            GenerateCode,
            "Database");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;

        var code = $$"""
            
            using System.Data;
            using Microsoft.Data.Sqlite;
            
            {{WriteFileScopedNamespace(ns)}}
            
            public interface IDbConnectionFactory
            {
                public Task<IDbConnection> CreateConnectionAsync();
            }
            
            public class SqliteConnectionFactory : IDbConnectionFactory
            {
                private readonly string _connectionString;
            
                public SqliteConnectionFactory(string connectionString)
                {
                    _connectionString = connectionString;
                }
            
                public async Task<IDbConnection> CreateConnectionAsync()
                {
                    var connection = new SqliteConnection(_connectionString);
                    await connection.OpenAsync();
                    return connection;
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
