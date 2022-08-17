using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Database;

[Generator]
public sealed class CreateEndpointGenerator : IIncrementalGenerator
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

        var type = enumerations[0];
        var code = GenerateCode(type);
        var typeNamespace = Utilities.GetRootNamespace(type) + ".Database";

        const string className = "DatabaseInitializer";

        context.AddSource($"{typeNamespace}.{className}.g.cs", code);
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Database" : null;
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using Dapper;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial class DatabaseInitializer
    {{
        private readonly IDbConnectionFactory _connectionFactory;

        public DatabaseInitializer(IDbConnectionFactory connectionFactory)
        {{
            _connectionFactory = connectionFactory;
        }}

        public async Task InitializeAsync()
        {{
            using var connection = await _connectionFactory.CreateConnectionAsync();
            // TODO: Generate SQL query for database creation
            // await connection.ExecuteAsync(@""CREATE TABLE IF NOT EXISTS Customers (
            // Id CHAR(36) PRIMARY KEY,
            // Username TEXT NOT NULL,
            // FullName TEXT NOT NULL,
            // Email TEXT NOT NULL,
            // DateOfBirth TEXT NOT NULL)"");
        }}
{(ns is null ? null : @"}
")}";
    }
}
