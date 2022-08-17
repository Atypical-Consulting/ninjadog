using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Database;

[Generator]
public sealed class DbConnectionFactoryGenerator : IIncrementalGenerator
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

        const string className = "DbConnectionFactory";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Database" : null;

        var code = @$"
using System.Data;
using Microsoft.Data.Sqlite;

{Utilities.WriteFileScopedNamespace(ns)}

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

        return Utilities.DefaultCodeLayout(code);
    }
}
