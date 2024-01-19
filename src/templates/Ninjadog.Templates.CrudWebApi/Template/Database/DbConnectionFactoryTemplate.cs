// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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

        return CreateNinjadogContentFile(fileName,
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
              """);
    }
}
