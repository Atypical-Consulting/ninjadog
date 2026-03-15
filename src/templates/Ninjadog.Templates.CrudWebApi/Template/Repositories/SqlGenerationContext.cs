namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// Encapsulates the settings needed for SQL generation across repository methods.
/// </summary>
internal readonly record struct SqlGenerationContext(
    bool SoftDelete,
    bool Auditing,
    string Provider)
{
    /// <summary>
    /// Gets a value indicating whether the provider is SQL Server.
    /// </summary>
    internal bool IsSqlServer => Provider == "sqlserver";

    /// <summary>
    /// Gets the SELECT TOP 1 prefix for single-row queries (SQL Server only).
    /// </summary>
    internal string SelectTopOne => IsSqlServer ? "TOP 1 " : string.Empty;

    /// <summary>
    /// Gets the LIMIT 1 suffix for single-row queries (non-SQL Server).
    /// </summary>
    internal string LimitOne => IsSqlServer ? string.Empty : " LIMIT 1";

    /// <summary>
    /// Gets the pagination clause for the provider.
    /// </summary>
    internal string PaginationClause => IsSqlServer
        ? " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"
        : " LIMIT @PageSize OFFSET @Offset";
}
