namespace Ninjadog.Templates.CrudWebAPI.Template.Endpoints;

/// <summary>
/// Base class for endpoint templates that caches auth and versioning settings
/// from the GenerateMany pipeline.
/// </summary>
public abstract class EndpointTemplateBase
    : NinjadogTemplate
{
    /// <summary>
    /// Gets a value indicating whether auth is enabled.
    /// </summary>
    protected bool HasAuth { get; private set; }

    /// <summary>
    /// Gets the optional API version number for versioned endpoints.
    /// </summary>
    protected int? ApiVersion { get; private set; }

    /// <inheritdoc />
    public override IEnumerable<NinjadogContentFile> GenerateMany(NinjadogSettings ninjadogSettings)
    {
        CacheSettings(ninjadogSettings);
        return base.GenerateMany(ninjadogSettings);
    }

    /// <summary>
    /// Caches auth and versioning settings from the provided configuration.
    /// Can be called by subclasses that override GenerateMany with custom iteration logic.
    /// </summary>
    /// <param name="ninjadogSettings">The settings to cache from.</param>
    protected void CacheSettings(NinjadogSettings ninjadogSettings)
    {
        HasAuth = ninjadogSettings.Config.Auth is not null;
        ApiVersion = ninjadogSettings.Config.Versioning?.DefaultVersion;
    }
}
