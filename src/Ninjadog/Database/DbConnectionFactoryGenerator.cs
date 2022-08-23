using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Database;

[Generator]
public sealed class DbConnectionFactoryGenerator : NinjadogBaseGenerator
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
        const string className = "DbConnectionFactory";

        context.AddSource(
            $"{GetRootNamespace(type)}.Database.{className}.g.cs",
            GenerateCode(type));
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Database" : null;

        var code = @$"
using System.Data;
using Microsoft.Data.Sqlite;

{WriteFileScopedNamespace(ns)}

public interface IDbConnectionFactory
{{
    public Task<IDbConnection> CreateConnectionAsync();
}}

public class SqliteConnectionFactory : IDbConnectionFactory
{{
    private readonly string _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {{
        _connectionString = connectionString;
    }}

    public async Task<IDbConnection> CreateConnectionAsync()
    {{
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
