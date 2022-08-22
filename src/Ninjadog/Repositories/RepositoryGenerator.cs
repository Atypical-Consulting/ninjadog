using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog.Repositories;

[Generator]
public sealed class RepositoryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modelTypes = Utilities.CollectNinjadogModelTypes(context);

        context.RegisterSourceOutput(modelTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Repositories";

            StringVariations sv = new(type.Name);
            var className = $"{sv.Pascal}Repository";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }


    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = Utilities.GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Repositories" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Contracts.Data;
using {rootNs}.Database;
using Dapper;

{Utilities.WriteFileScopedNamespace(ns)}

public partial class {_.ClassModelRepository} : {_.InterfaceModelRepository}
{{
    private readonly IDbConnectionFactory _connectionFactory;

    public {_.ClassModelRepository}(IDbConnectionFactory connectionFactory)
    {{
        _connectionFactory = connectionFactory;
    }}

    public async Task<bool> CreateAsync({_.ClassModelDto} {_.VarModel})
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @""INSERT INTO Customers (Id, Username, FullName, Email, DateOfBirth)
            VALUES (@Id, @Username, @FullName, @Email, @DateOfBirth)"",
            {_.VarModel});
        return result > 0;
    }}

    public async Task<{_.ClassModelDto}?> GetAsync(Guid id)
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<{_.ClassModelDto}>(
            ""SELECT * FROM Customers WHERE Id = @Id LIMIT 1"", new {{ Id = id.ToString() }});
    }}

    public async Task<IEnumerable<{_.ClassModelDto}>> GetAllAsync()
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<{_.ClassModelDto}>(""SELECT * FROM Customers"");
    }}

    public async Task<bool> UpdateAsync({_.ClassModelDto} {_.VarModel})
    {{
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @""UPDATE Customers SET Username = @Username, FullName = @FullName, Email = @Email,
                 DateOfBirth = @DateOfBirth WHERE Id = @Id"",
            {_.VarModel});
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

        return Utilities.DefaultCodeLayout(code);
    }
}
