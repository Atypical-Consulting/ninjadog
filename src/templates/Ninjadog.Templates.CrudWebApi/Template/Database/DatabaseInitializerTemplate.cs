// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;
using Ninjadog.Helpers;

namespace Ninjadog.Templates.CrudWebAPI.Template.Database;

/// <summary>
/// This template generates the DatabaseInitializer class.
/// </summary>
public sealed class DatabaseInitializerTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(
        NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        var ns = $"{rootNamespace}.Database";
        const string fileName = "DatabaseInitializer.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial class DatabaseInitializer
              {
                  private readonly IDbConnectionFactory _connectionFactory;

                  public DatabaseInitializer(IDbConnectionFactory connectionFactory)
                  {
                      _connectionFactory = connectionFactory;
                  }

                  public async Task InitializeAsync()
                  {
                      using var connection = await _connectionFactory.CreateConnectionAsync();
                      {{GenerateCreateTableSqlQueries(entities)}}
                  }
              }
              """);
    }

    private static string GenerateCreateTableSqlQueries(List<NinjadogEntityWithKey> entities)
    {
        IndentedStringBuilder sb = new(2);

        foreach (var entity in entities)
        {
            sb.AppendLine();
            sb.AppendLine($"await connection.ExecuteAsync(@\"{GenerateSqlCreateTableQuery(entity)}\");");
        }

        return sb.ToString();
    }

    private static string GenerateSqlCreateTableQuery(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        IndentedStringBuilder sb = new(0);

        sb.AppendLine($"CREATE TABLE IF NOT EXISTS {st.Models} (");
        sb.IncrementIndent().IncrementIndent().IncrementIndent();
        sb.AppendLine("Id CHAR(36) PRIMARY KEY,");

        // Using LINQ to filter out ID property and then joining them with String.Join
        var columnDefinitions = entity.Properties
            .Where(p => !p.Value.IsKey)
            .Select(p => $"{p.Key} TEXT NOT NULL");

        sb.Append(string.Join(",\n", columnDefinitions));
        sb.Append(")");

        return sb.ToString();
    }

}
