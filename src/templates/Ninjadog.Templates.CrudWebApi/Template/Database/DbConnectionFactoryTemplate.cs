// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.Template.Database;

/// <summary>
/// This template generates the DbConnectionFactory class.
/// </summary>
public sealed class DbConnectionFactoryTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "DbConnectionFactory";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var ns = $"{rootNamespace}.Database";
        const string fileName = "DbConnectionFactory.cs";

        var content =
            $$"""

              using System.Data;
              using Microsoft.Data.Sqlite;

              {{WriteFileScopedNamespace(ns)}}

              public interface IDbConnectionFactory
              {
                  public Task<IDbConnection> CreateConnectionAsync();
              }

              /// <summary>
              /// This class provides a factory for database connections.
              /// </summary>
              /// <param name="connectionString">The connection string to use.</param>
              public class SqliteConnectionFactory(string connectionString)
                  : IDbConnectionFactory
              {
                  public async Task<IDbConnection> CreateConnectionAsync()
                  {
                      var connection = new SqliteConnection(connectionString);
                      await connection.OpenAsync();
                      return connection;
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }
}
