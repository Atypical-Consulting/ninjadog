// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Database;

[Generator]
public sealed class DbConnectionFactoryGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            "DbConnectionFactory",
            GenerateCode,
            "Database");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;

        var code = $$"""

            using System.Data;
            using Microsoft.Data.Sqlite;

            {{WriteFileScopedNamespace(ns)}}

            public interface IDbConnectionFactory
            {
                public Task<IDbConnection> CreateConnectionAsync();
            }

            public class SqliteConnectionFactory : IDbConnectionFactory
            {
                private readonly string _connectionString;

                public SqliteConnectionFactory(string connectionString)
                {
                    _connectionString = connectionString;
                }

                public async Task<IDbConnection> CreateConnectionAsync()
                {
                    var connection = new SqliteConnection(_connectionString);
                    await connection.OpenAsync();
                    return connection;
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
