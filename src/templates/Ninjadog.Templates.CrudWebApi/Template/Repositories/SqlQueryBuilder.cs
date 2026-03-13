using Ninjadog.Templates.CrudWebAPI.Template.Database;

namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// Centralizes SQL query generation for repository templates.
/// Handles provider-specific SQL, soft delete, and auditing concerns.
/// </summary>
internal static class SqlQueryBuilder
{
    /// <summary>
    /// Generates the allowed columns HashSet initializer for filtering and sorting.
    /// </summary>
    /// <param name="entity">The entity to generate columns for.</param>
    /// <returns>The comma-separated quoted column names.</returns>
    internal static string GenerateAllowedColumns(NinjadogEntityWithKey entity)
    {
        var columns = entity.Properties
            .Where(p => !p.Value.IsKey)
            .Select(p => $"\"{p.Key.UppercaseFirst()}\"");
        return string.Join(", ", columns);
    }

    /// <summary>
    /// Generates an INSERT SQL statement for the entity.
    /// </summary>
    /// <param name="entity">The entity to generate the query for.</param>
    /// <param name="sql">The SQL generation context with provider/feature flags.</param>
    /// <returns>The INSERT SQL statement.</returns>
    internal static string GenerateInsertQuery(NinjadogEntityWithKey entity, SqlGenerationContext sql)
    {
        var st = entity.StringTokens;
        var properties = entity.Properties;

        var columns = properties.Keys.Select(k => k.UppercaseFirst()).ToList();
        var values = properties.Keys.Select(k => $"@{k.UppercaseFirst()}").ToList();

        if (sql.Auditing)
        {
            columns.Add("CreatedAt");
            columns.Add("UpdatedAt");
            var nowFunc = DatabaseProviderHelper.GetNowFunction(sql.Provider);
            values.Add(nowFunc);
            values.Add(nowFunc);
        }

        return new IndentedStringBuilder(0)
            .Append($"INSERT INTO {st.Models} (")
            .Append(string.Join(", ", columns))
            .Append(") ")
            .IncrementIndent(3)
            .Append("VALUES (")
            .Append(string.Join(", ", values))
            .Append(")")
            .ToString();
    }

    /// <summary>
    /// Generates a SELECT single-row SQL statement for the entity.
    /// </summary>
    /// <param name="entity">The entity to generate the query for.</param>
    /// <param name="sql">The SQL generation context with provider/feature flags.</param>
    /// <returns>The SELECT SQL statement.</returns>
    internal static string GenerateSelectOneQuery(NinjadogEntityWithKey entity, SqlGenerationContext sql)
    {
        var st = entity.StringTokens;
        var entityKey = entity.Properties.GetEntityKey();
        var softDeleteFilter = sql.SoftDelete ? " AND IsDeleted = 0" : string.Empty;
        var limit = sql.Provider == "sqlserver" ? string.Empty : " LIMIT 1";
        var top = sql.Provider == "sqlserver" ? "TOP 1 " : string.Empty;
        return $"SELECT {top}* FROM {st.Models} WHERE {entityKey.PascalKey} = @{entityKey.PascalKey}{softDeleteFilter}{limit}";
    }

    /// <summary>
    /// Generates the GetAll SQL builder code block for the repository.
    /// </summary>
    /// <param name="entity">The entity to generate the query for.</param>
    /// <param name="sql">The SQL generation context with provider/feature flags.</param>
    /// <returns>The C# code block that builds the GetAll SQL at runtime.</returns>
    internal static string GenerateGetAllSqlBuilder(NinjadogEntityWithKey entity, SqlGenerationContext sql)
    {
        var st = entity.StringTokens;
        var baseQuery = sql.SoftDelete
            ? $"SELECT * FROM {st.Models} WHERE IsDeleted = 0"
            : $"SELECT * FROM {st.Models}";
        var paginationClause = sql.Provider == "sqlserver"
            ? " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"
            : " LIMIT @PageSize OFFSET @Offset";

        var lines = GenerateFilterableQueryLines(baseQuery, sql.SoftDelete);
        lines.Add(string.Empty);

        lines.AddRange(sql.Provider == "sqlserver"
            ?
            [
                "sql += !string.IsNullOrEmpty(orderBy)",
                "    ? \" ORDER BY \" + orderBy",
                "    : \" ORDER BY (SELECT NULL)\";",
                string.Empty,
            ]
            :
            [
                "if (!string.IsNullOrEmpty(orderBy))",
                "{",
                "    sql += \" ORDER BY \" + orderBy;",
                "}",
                string.Empty,
            ]);

        lines.Add($"sql += \"{paginationClause}\";");

        return string.Join("\n        ", lines);
    }

    /// <summary>
    /// Generates the Count SQL builder code block for the repository.
    /// </summary>
    /// <param name="entity">The entity to generate the query for.</param>
    /// <param name="softDelete">Whether soft delete is enabled.</param>
    /// <returns>The C# code block that builds the COUNT SQL at runtime.</returns>
    internal static string GenerateCountSqlBuilder(NinjadogEntityWithKey entity, bool softDelete)
    {
        var st = entity.StringTokens;
        var baseQuery = softDelete
            ? $"SELECT COUNT(*) FROM {st.Models} WHERE IsDeleted = 0"
            : $"SELECT COUNT(*) FROM {st.Models}";

        var lines = GenerateFilterableQueryLines(baseQuery, softDelete);
        return string.Join("\n        ", lines);
    }

    /// <summary>
    /// Generates an UPDATE SQL statement for the entity.
    /// </summary>
    /// <param name="entity">The entity to generate the query for.</param>
    /// <param name="sql">The SQL generation context with provider/feature flags.</param>
    /// <returns>The UPDATE SQL statement.</returns>
    internal static string GenerateUpdateQuery(NinjadogEntityWithKey entity, SqlGenerationContext sql)
    {
        var st = entity.StringTokens;
        var properties = entity.Properties;
        var entityKey = entity.Properties.GetEntityKey();

        IndentedStringBuilder stringBuilder = new(0);

        stringBuilder.Append($"UPDATE {st.Models} SET ");

        var updateClauses = properties
            .Where(p => !p.Value.IsKey)
            .Select(p => $"{p.Key.UppercaseFirst()} = @{p.Key.UppercaseFirst()}")
            .ToList();

        if (sql.Auditing)
        {
            updateClauses.Add($"UpdatedAt = {DatabaseProviderHelper.GetNowFunction(sql.Provider)}");
        }

        return stringBuilder
            .Append(string.Join(", ", updateClauses))
            .Append($" WHERE {entityKey.PascalKey} = @{entityKey.PascalKey}")
            .ToString();
    }

    /// <summary>
    /// Generates a DELETE (or soft-delete UPDATE) SQL statement for the entity.
    /// </summary>
    /// <param name="entity">The entity to generate the query for.</param>
    /// <param name="sql">The SQL generation context with provider/feature flags.</param>
    /// <returns>The DELETE or UPDATE SQL statement.</returns>
    internal static string GenerateDeleteQuery(NinjadogEntityWithKey entity, SqlGenerationContext sql)
    {
        var st = entity.StringTokens;
        var entityKey = entity.Properties.GetEntityKey();
        return sql.SoftDelete
            ? $"UPDATE {st.Models} SET IsDeleted = 1, DeletedAt = {DatabaseProviderHelper.GetNowFunction(sql.Provider)} WHERE {entityKey.PascalKey} = @{entityKey.PascalKey}"
            : $"DELETE FROM {st.Models} WHERE {entityKey.PascalKey} = @{entityKey.PascalKey}";
    }

    private static List<string> GenerateFilterableQueryLines(string baseQuery, bool softDelete)
    {
        var filterJoin = softDelete ? " AND " : " WHERE ";
        return
        [
            $"var sql = \"{baseQuery}\";",
            "if (!string.IsNullOrEmpty(whereClause))",
            "{",
            $"    sql += \"{filterJoin}\" + whereClause;",
            "}",
        ];
    }
}
