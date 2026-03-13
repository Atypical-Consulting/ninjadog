namespace Ninjadog.Templates.CrudWebAPI.Template.Database;

/// <summary>
/// Shared utilities for database provider-specific SQL generation.
/// </summary>
internal static class DatabaseProviderHelper
{
    /// <summary>
    /// Returns the SQL function for getting the current timestamp for the given provider.
    /// </summary>
    internal static string GetNowFunction(string provider)
    {
        return provider switch
        {
            "postgresql" => "NOW()",
            "sqlserver" => "GETUTCDATE()",
            _ => "datetime('now')"
        };
    }

    /// <summary>
    /// Returns the provider-specific idempotent INSERT statement.
    /// </summary>
    internal static string GetIdempotentInsert(
        string provider,
        string tableName,
        string columns,
        string paramNames,
        string keyPropertyName)
    {
        return provider switch
        {
            "postgresql" => $"INSERT INTO {tableName} ({columns}) VALUES ({paramNames}) ON CONFLICT DO NOTHING",
            "sqlserver" => $"IF NOT EXISTS (SELECT 1 FROM {tableName} WHERE {keyPropertyName} = @{keyPropertyName}) INSERT INTO {tableName} ({columns}) VALUES ({paramNames})",
            _ => $"INSERT OR IGNORE INTO {tableName} ({columns}) VALUES ({paramNames})"
        };
    }

    /// <summary>
    /// Returns the provider-specific connection factory details (using directive, class name, connection type).
    /// </summary>
    internal static (string UsingDirective, string ClassName, string ConnectionType) GetConnectionFactoryDetails(string provider)
    {
        return provider switch
        {
            "postgresql" => ("using Npgsql;", "NpgsqlConnectionFactory", "NpgsqlConnection"),
            "sqlserver" => ("using Microsoft.Data.SqlClient;", "SqlServerConnectionFactory", "SqlConnection"),
            _ => ("using Microsoft.Data.Sqlite;", "SqliteConnectionFactory", "SqliteConnection")
        };
    }
}
