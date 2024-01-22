// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// This template generates the repository for a given entity.
/// </summary>
public sealed class RepositoryTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "Repository";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOneByEntity(
        NinjadogEntityWithKey entity, string rootNamespace)
    {
        var st = entity.StringTokens;
        var ns = $"{rootNamespace}.Repositories";
        var fileName = $"{st.ClassModelRepository}.cs";

        var content =
            $$"""

              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Database;
              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial class {{st.ClassModelRepository}}(IDbConnectionFactory connectionFactory)
                  : {{st.InterfaceModelRepository}}
              {
                  public async Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}})
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var result = await connection.ExecuteAsync(
                          @"{{GenerateSqlInsertQuery(entity)}}",
                          {{st.VarModel}});

                      return result > 0;
                  }

                  public async Task<{{st.ClassModelDto}}?> GetAsync(Guid id)
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      return await connection.QuerySingleOrDefaultAsync<{{st.ClassModelDto}}>(
                          "{{GenerateSqlSelectOneQuery(entity)}}",
                          new { Id = id.ToString() });
                  }

                  public async Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync()
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();
                      return await connection.QueryAsync<{{st.ClassModelDto}}>("{{GenerateSqlSelectAllQuery(entity)}}");
                  }

                  public async Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}})
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var result = await connection.ExecuteAsync(
                          @"{{GenerateSqlUpdateQuery(entity)}}",
                          {{st.VarModel}});

                          return result > 0;
                  }

                  public async Task<bool> DeleteAsync(Guid id)
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();

                      var result = await connection.ExecuteAsync(
                          @"{{GenerateSqlDeleteQuery(entity)}}",
                          new {Id = id.ToString()});

                      return result > 0;
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }

    private static string GenerateSqlInsertQuery(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var properties = entity.Properties;

        IndentedStringBuilder sb = new(0);
        sb.Append($"INSERT INTO {st.Models} (");

        // Using String.Join to handle the comma-separated list
        sb.Append(string.Join(", ", properties.Keys));
        sb.Append(") ");

        sb.IncrementIndent().IncrementIndent().IncrementIndent();
        sb.Append("VALUES (");

        // Again using String.Join for the values
        sb.Append(string.Join(", ", properties.Keys.Select(k => $"@{k}")));
        sb.Append(")");

        return sb.ToString();
    }

    private static string GenerateSqlSelectOneQuery(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        return $"SELECT * FROM {st.Models} WHERE Id = @Id LIMIT 1";
    }

    private static string GenerateSqlSelectAllQuery(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        return $"SELECT * FROM {st.Models}";
    }

    private static string GenerateSqlUpdateQuery(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var properties = entity.Properties;

        IndentedStringBuilder sb = new(0);

        sb.Append($"UPDATE {st.Models} SET ");

        // Using LINQ to filter out key properties and then joining them with String.Join
        var updateClauses = properties
            .Where(p => !p.Value.IsKey)
            .Select(p => $"{p.Key} = @{p.Key}");

        sb.Append(string.Join(", ", updateClauses));
        sb.Append(" WHERE Id = @Id"); // Assuming 'Id' is the primary key column name

        return sb.ToString();
    }

    private static string GenerateSqlDeleteQuery(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        return $"DELETE FROM {st.Models} WHERE Id = @Id";
    }
}
