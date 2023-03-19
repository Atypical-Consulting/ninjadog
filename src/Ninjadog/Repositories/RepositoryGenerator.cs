namespace Ninjadog.Repositories;

[Generator]
public sealed class RepositoryGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new GeneratorSetup(
            st => $"{st.Model}Repository",
            GenerateCode,
            "Repositories");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""

            using {{rootNs}}.Contracts.Data;
            using {{rootNs}}.Database;
            using Dapper;

            {{WriteFileScopedNamespace(ns)}}

            public partial class {{st.ClassModelRepository}} : {{st.InterfaceModelRepository}}
            {
                private readonly IDbConnectionFactory _connectionFactory;

                public {{st.ClassModelRepository}}(IDbConnectionFactory connectionFactory)
                {
                    _connectionFactory = connectionFactory;
                }

                public async Task<bool> CreateAsync({{st.ClassModelDto}} {{st.VarModel}})
                {
                    using var connection = await _connectionFactory.CreateConnectionAsync();

                    var result = await connection.ExecuteAsync(
                        @"{{GenerateSqlInsertQuery(typeContext)}}",
                        {{st.VarModel}});

                    return result > 0;
                }

                public async Task<{{st.ClassModelDto}}?> GetAsync(Guid id)
                {
                    using var connection = await _connectionFactory.CreateConnectionAsync();

                    return await connection.QuerySingleOrDefaultAsync<{{st.ClassModelDto}}>(
                        "{{GenerateSqlSelectOneQuery(typeContext)}}",
                        new { Id = id.ToString() });
                }

                public async Task<IEnumerable<{{st.ClassModelDto}}>> GetAllAsync()
                {
                    using var connection = await _connectionFactory.CreateConnectionAsync();
                    return await connection.QueryAsync<{{st.ClassModelDto}}>("{{GenerateSqlSelectAllQuery(typeContext)}}");
                }

                public async Task<bool> UpdateAsync({{st.ClassModelDto}} {{st.VarModel}})
                {
                    using var connection = await _connectionFactory.CreateConnectionAsync();

                    var result = await connection.ExecuteAsync(
                        @"{{GenerateSqlUpdateQuery(typeContext)}}",
                        {{st.VarModel}});

                        return result > 0;
                }

                public async Task<bool> DeleteAsync(Guid id)
                {
                    using var connection = await _connectionFactory.CreateConnectionAsync();

                    var result = await connection.ExecuteAsync(
                        @"{{GenerateSqlDeleteQuery(typeContext)}}",
                        new {Id = id.ToString()});

                    return result > 0;
                }
            }
            """;

        return DefaultCodeLayout(code);
    }

    private static string GenerateSqlInsertQuery(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        var properties = typeContext.PropertyContexts;

        IndentedStringBuilder sb = new();
        sb.Append($"INSERT INTO {st.Models} (");

        foreach (var context in properties)
        {
            sb.Append($"{context.Name}");

            if (!context.IsLast)
            {
                sb.Append(", ");
            }
            else
            {
                sb.Append(") ");
            }
        }

        sb.IncrementIndent().IncrementIndent().IncrementIndent();
        sb.Append("VALUES (");

        foreach (var context in properties)
        {
            sb.Append($"@{context.Name}");

            if (!context.IsLast)
            {
                sb.Append(", ");
            }
            else
            {
                sb.Append(")");
            }
        }

        return sb.ToString();
    }

    private static string GenerateSqlSelectOneQuery(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        return $"SELECT * FROM {st.Models} WHERE Id = @Id LIMIT 1";
    }

    private static string GenerateSqlSelectAllQuery(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        return $"SELECT * FROM {st.Models}";
    }

    private static string GenerateSqlUpdateQuery(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        var properties = typeContext.PropertyContexts;

        IndentedStringBuilder sb = new();

        sb.Append($"UPDATE {st.Models} SET ");

        foreach (var context in properties.Where(context => !context.IsId))
        {
            sb.Append($"{context.Name} = @{context.Name}");
            sb.Append(!context.IsLast ? ", " : " WHERE Id = @Id");
        }

        return sb.ToString();
    }

    private static string GenerateSqlDeleteQuery(TypeContext typeContext)
    {
        var st = typeContext.Tokens;
        return $"DELETE FROM {st.Models} WHERE Id = @Id";
    }
}
