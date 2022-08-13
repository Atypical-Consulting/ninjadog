using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Mapping;

[Generator]
public sealed class ApiContractToDomainMapperGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ITypeSymbol>> enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(Utilities.CouldBeEnumerationAsync, Utilities.GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;

        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> enumerations)
    {
        if (enumerations.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in enumerations)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Mapping";

            context.AddSource($"{typeNamespace}.{type.Name}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Mapping" : null;
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using System.Collections.Generic;
using {rootNs}.Database;
using Dapper;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public class {name}Repository : I{name}Repository
    {{
        private readonly IDbConnectionFactory _connectionFactory;

        public {name}Repository(IDbConnectionFactory connectionFactory)
        {{
            _connectionFactory = connectionFactory;
        }}

        public async Task<bool> CreateAsync({dto} {lower})
        {{
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @""INSERT INTO Customers (Id, Username, FullName, Email, DateOfBirth)
                VALUES (@Id, @Username, @FullName, @Email, @DateOfBirth)"",
                {lower});
            return result > 0;
        }}

        public async Task<{dto}?> GetAsync(Guid id)
        {{
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<{dto}>(
                ""SELECT * FROM Customers WHERE Id = @Id LIMIT 1"", new {{ Id = id.ToString() }});
        }}

        public async Task<IEnumerable<{dto}>> GetAllAsync()
        {{
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<{dto}>(""SELECT * FROM Customers"");
        }}

        public async Task<bool> UpdateAsync({dto} {lower})
        {{
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @""UPDATE Customers SET Username = @Username, FullName = @FullName, Email = @Email,
                     DateOfBirth = @DateOfBirth WHERE Id = @Id"",
                {lower});
            return result > 0;
        }}

        public async Task<bool> DeleteAsync(Guid id)
        {{
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(@""DELETE FROM Customers WHERE Id = @Id"",
                new {{Id = id.ToString()}});
            return result > 0;
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
