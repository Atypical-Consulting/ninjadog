// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Database;

[Generator]
public sealed class DatabaseInitializerGenerator : NinjadogIncrementalGeneratorBase
{
    /// <inheritdoc />
    protected override IncrementalGeneratorSetup Setup
        => new(
            "DatabaseInitializer",
            GenerateCode,
            "Database");

    private static string GenerateCode(ImmutableArray<TypeContext> typeContexts)
    {
        var typeContext = typeContexts[0];
        var ns = typeContext.Ns;

        var code = $$"""

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
                    {{GenerateCreateTableSqlQueries(typeContexts)}}
                }
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateCreateTableSqlQueries(ImmutableArray<TypeContext> immutableArray)
    {
        IndentedStringBuilder sb = new(2);

        foreach (var typeContext in immutableArray)
        {
            sb.AppendLine();
            sb.AppendLine($"await connection.ExecuteAsync(@\"{GenerateSqlCreateTableQuery(typeContext)}\");");
        }

        return sb.ToString();
    }

    private static string GenerateSqlCreateTableQuery(TypeContext typeContext)
    {
        var st = typeContext.Tokens;

        IndentedStringBuilder sb = new(0);

        sb.AppendLine($"CREATE TABLE IF NOT EXISTS {st.Models} (");
        sb.IncrementIndent().IncrementIndent().IncrementIndent();
        sb.AppendLine("Id CHAR(36) PRIMARY KEY,");

        foreach (var context in typeContext.PropertyContexts.Where(context => !context.IsId))
        {
            sb.Append($"{context.Name} TEXT NOT NULL");

            if (!context.IsLast)
            {
                sb.AppendLine(",");
            }
            else
            {
                sb.Append(")");
            }
        }

        return sb.ToString();
    }
}
