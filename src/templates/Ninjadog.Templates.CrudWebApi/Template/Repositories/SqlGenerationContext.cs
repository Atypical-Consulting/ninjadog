namespace Ninjadog.Templates.CrudWebAPI.Template.Repositories;

/// <summary>
/// Encapsulates the settings needed for SQL generation across repository methods.
/// </summary>
internal readonly record struct SqlGenerationContext(
    bool SoftDelete,
    bool Auditing,
    string Provider);
