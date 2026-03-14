namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// This template generates the repository for a given entity.
/// </summary>
public sealed class RepositoryTemplate
    : NinjadogTemplate
{
    private SqlGenerationContext _sql;
    private bool _aot;

    /// <inheritdoc />
    public override string Name => "Repository";

    /// <inheritdoc />
    public override IEnumerable<NinjadogContentFile> GenerateMany(NinjadogSettings ninjadogSettings)
    {
        _sql = new SqlGenerationContext(
            ninjadogSettings.Config.SoftDelete,
            ninjadogSettings.Config.Auditing,
            ninjadogSettings.Config.DatabaseProvider);
        _aot = ninjadogSettings.Config.Aot;
        return base.GenerateMany(ninjadogSettings);
    }

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Repositories";
        var fileName = $"{st.ClassModelRepository}.cs";
        var entityKey = entity.Properties.GetEntityKey();

        var dapperAotAttribute = _aot ? "\n[DapperAot]" : string.Empty;

        var content =
            $$"""

              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}
              {{dapperAotAttribute}}
              public partial class {{st.ClassModelRepository}}(IDbConnectionFactory connectionFactory)
                  : {{st.InterfaceModelRepository}}
              {
                  public async Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}})
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var result = await connection.ExecuteAsync(
                          @"{{SqlQueryBuilder.GenerateInsertQuery(entity, _sql)}}",
                          {{st.VarModel}});

                      return result > 0;
                  }

                  public async Task<{{st.ClassModelDto}}?> GetAsync({{entityKey.Type}} id)
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      return await connection.QuerySingleOrDefaultAsync<{{st.ClassModelDto}}>(
                          "{{SqlQueryBuilder.GenerateSelectOneQuery(entity, _sql)}}",
                          new { {{entityKey.PascalKey}} = id });
                  }

                  public async Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync(
                      int page, int pageSize,
                      Dictionary<string, string>? filters = null,
                      string? sortBy = null,
                      bool sortDescending = false)
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var (whereClause, parameters) = BuildWhereClause(filters);
                      parameters.Add("PageSize", pageSize);
                      parameters.Add("Offset", (page - 1) * pageSize);

                      var orderBy = BuildOrderByClause(sortBy, sortDescending);
                      {{SqlQueryBuilder.GenerateGetAllSqlBuilder(entity, _sql)}}

                      return await connection.QueryAsync<{{st.ClassModelDto}}>(sql, parameters);
                  }

                  public async Task<int> CountAsync(Dictionary<string, string>? filters = null)
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var (whereClause, parameters) = BuildWhereClause(filters);
                      {{SqlQueryBuilder.GenerateCountSqlBuilder(entity, _sql.SoftDelete)}}

                      return await connection.ExecuteScalarAsync<int>(sql, parameters);
                  }

                  public async Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}})
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var result = await connection.ExecuteAsync(
                          @"{{SqlQueryBuilder.GenerateUpdateQuery(entity, _sql)}}",
                          {{st.VarModel}});

                          return result > 0;
                  }

                  public async Task<bool> DeleteAsync({{entityKey.Type}} id)
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var result = await connection.ExecuteAsync(
                          @"{{SqlQueryBuilder.GenerateDeleteQuery(entity, _sql)}}",
                          new { {{entityKey.PascalKey}} = id });

                      return result > 0;
                  }

                  private static readonly HashSet<string> AllowedColumns = new(StringComparer.OrdinalIgnoreCase)
                  {
                      {{SqlQueryBuilder.GenerateAllowedColumns(entity)}}
                  };

                  private static (string WhereClause, Dictionary<string, object> Parameters) BuildWhereClause(
                      Dictionary<string, string>? filters)
                  {
                      var parameters = new Dictionary<string, object>();
                      if (filters is null || filters.Count == 0)
                      {
                          return (string.Empty, parameters);
                      }

                      var conditions = new List<string>();
                      foreach (var (key, value) in filters)
                      {
                          if (AllowedColumns.Contains(key))
                          {
                              conditions.Add($"{key} = @Filter_{key}");
                              parameters[$"Filter_{key}"] = value;
                          }
                      }

                      var whereClause = conditions.Count > 0
                          ? string.Join(" AND ", conditions)
                          : string.Empty;

                      return (whereClause, parameters);
                  }

                  private static string BuildOrderByClause(string? sortBy, bool sortDescending)
                  {
                      if (string.IsNullOrEmpty(sortBy) || !AllowedColumns.Contains(sortBy))
                      {
                          return string.Empty;
                      }

                      var direction = sortDescending ? "DESC" : "ASC";
                      return $"{sortBy} {direction}";
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
