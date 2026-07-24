namespace Ninjadog.Templates.CrudWebAPI.Template.Database;

/// <summary>
/// This template generates the DatabaseInitializer class.
/// </summary>
public sealed class DatabaseInitializerTemplate
    : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "DatabaseInitializer";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        var enumNames = ninjadogSettings.Enums?.Keys.ToHashSet();
        var softDelete = ninjadogSettings.Config.SoftDelete;
        var auditing = ninjadogSettings.Config.Auditing;
        var provider = ninjadogSettings.Config.DatabaseProvider;
        var ns = $"{rootNamespace}.Database";
        const string fileName = "DatabaseInitializer.cs";

        var content =
            $$"""

              using Dapper;

              {{WriteFileScopedNamespace(ns)}}

              public partial class DatabaseInitializer(IDbConnectionFactory connectionFactory)
              {
                  public async Task InitializeAsync()
                  {
                      using var connection = await connectionFactory.CreateConnectionAsync();
                      {{GenerateCreateTableSqlQueries(entities, enumNames, softDelete, auditing, provider)}}
                  }
              }
              """;

        return CreateNinjadogContentFile(fileName, content);
    }

    private static string GenerateCreateTableSqlQueries(List<NinjadogEntityWithKey> entities, HashSet<string>? enumNames, bool softDelete, bool auditing, string provider)
    {
        IndentedStringBuilder stringBuilder = new(2);

        foreach (var entity in entities)
        {
            stringBuilder
                .AppendLine()
                .AppendLine($"await connection.ExecuteAsync(@\"{GenerateSqlCreateTableQuery(entity, entities, enumNames, softDelete, auditing, provider)}\");");
        }

        return stringBuilder.ToString();
    }

    private static string GenerateSqlCreateTableQuery(NinjadogEntityWithKey entity, List<NinjadogEntityWithKey> allEntities, HashSet<string>? enumNames, bool softDelete, bool auditing, string provider)
    {
        var st = entity.StringTokens;
        var entityKey = entity.Properties.GetEntityKey();
        var fkConstraints = GetForeignKeyConstraints(entity, allEntities);

        // Collect all column definitions into a flat list
        List<string> columns =
        [
            $"{entityKey.PascalKey} {MapToDbType(entityKey.Type, provider, enumNames)} PRIMARY KEY",
        ];

        // Non-key properties
        foreach (var p in entity.Properties.Where(p => !p.Value.IsKey))
        {
            var nullConstraint = p.Value.Required ? " NOT NULL" : string.Empty;
            columns.Add($"{p.Key.UppercaseFirst()} {MapToDbType(p.Value.Type, provider, enumNames)}{nullConstraint}");
        }

        // Soft delete columns
        if (softDelete)
        {
            columns.Add("IsDeleted INTEGER NOT NULL DEFAULT 0");
            columns.Add("DeletedAt TEXT");
        }

        // Audit columns
        if (auditing)
        {
            columns.Add("CreatedAt TEXT NOT NULL");
            columns.Add("UpdatedAt TEXT");
        }

        // Foreign key constraints
        foreach (var (fkColumn, parentTable, parentPk) in fkConstraints)
        {
            columns.Add($"FOREIGN KEY ({fkColumn}) REFERENCES {parentTable}({parentPk})");
        }

        // Build the CREATE TABLE statement
        IndentedStringBuilder stringBuilder = new(0);
        stringBuilder
            .AppendLine($"CREATE TABLE IF NOT EXISTS {st.Models} (")
            .IncrementIndent().IncrementIndent().IncrementIndent();

        for (var i = 0; i < columns.Count; i++)
        {
            var isLast = i == columns.Count - 1;
            if (isLast)
            {
                stringBuilder.Append($"{columns[i]})");
            }
            else
            {
                stringBuilder.AppendLine($"{columns[i]},");
            }
        }

        return stringBuilder.ToString();
    }

    private static List<(string FkColumn, string ParentTable, string ParentPk)> GetForeignKeyConstraints(
        NinjadogEntityWithKey entity, List<NinjadogEntityWithKey> allEntities)
    {
        var constraints = new List<(string FkColumn, string ParentTable, string ParentPk)>();

        foreach (var potentialParent in allEntities)
        {
            if (potentialParent.Relationships == null)
            {
                continue;
            }

            foreach (var (_, relationship) in potentialParent.Relationships)
            {
                if (relationship.RelatedEntity != entity.Key)
                {
                    continue;
                }

                if (relationship.RelationshipType is not (NinjadogEntityRelationshipType.OneToMany or NinjadogEntityRelationshipType.OneToOne))
                {
                    continue;
                }

                var parentPk = potentialParent.Properties.GetEntityKey();
                var fkColumnName = parentPk.Key == "Id"
                    ? $"{potentialParent.Key}Id"
                    : parentPk.Key;

                if (entity.Properties.ContainsKey(fkColumnName))
                {
                    constraints.Add((fkColumnName.UppercaseFirst(), potentialParent.StringTokens.Models, parentPk.PascalKey));
                }
            }
        }

        return constraints;
    }

    private static string MapToDbType(string typeName, string provider, HashSet<string>? enumNames = null)
    {
        return DatabaseProviderHelper.MapToDbType(typeName, provider, enumNames);
    }
}
