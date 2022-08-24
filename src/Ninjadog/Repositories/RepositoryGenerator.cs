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

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Database;
using Dapper;

{WriteFileScopedNamespace(ns)}

public partial class {st.ClassModelRepository} : {st.InterfaceModelRepository}
{{
    private readonly IDbConnectionFactory _connectionFactory;

    public {st.ClassModelRepository}(IDbConnectionFactory connectionFactory)
    {{
        _connectionFactory = connectionFactory;
    }}

    public async Task<bool> CreateAsync({st.ClassModelDto} {st.VarModel})
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @""INSERT INTO Customers (Id, Username, FullName, Email, DateOfBirth)
            VALUES (@Id, @Username, @FullName, @Email, @DateOfBirth)"",
            {st.VarModel});
        return result > 0;
    }}

    public async Task<{st.ClassModelDto}?> GetAsync(Guid id)
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<{st.ClassModelDto}>(
            ""SELECT * FROM Customers WHERE Id = @Id LIMIT 1"", new {{ Id = id.ToString() }});
    }}

    public async Task<IEnumerable<{st.ClassModelDto}>> GetAllAsync()
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<{st.ClassModelDto}>(""SELECT * FROM Customers"");
    }}

    public async Task<bool> UpdateAsync({st.ClassModelDto} {st.VarModel})
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @""UPDATE Customers SET Username = @Username, FullName = @FullName, Email = @Email,
                 DateOfBirth = @DateOfBirth WHERE Id = @Id"",
            {st.VarModel});
        return result > 0;
    }}

    public async Task<bool> DeleteAsync(Guid id)
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@""DELETE FROM Customers WHERE Id = @Id"",
            new {{Id = id.ToString()}});
        return result > 0;
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
