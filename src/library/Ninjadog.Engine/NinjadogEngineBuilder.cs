using Microsoft.Extensions.DependencyInjection;

namespace Ninjadog.Engine;

/// <summary>
/// Represents a builder responsible for constructing and configuring instances of the Ninjadog Engine.
/// </summary>
public sealed class NinjadogEngineBuilder
    : INinjadogEngineBuilder
{
    private NinjadogTemplateManifest? _templateManifest;
    private NinjadogSettings? _ninjadogSettings;
    private NinjadogOutputProcessors? _outputProcessors;
    private IServiceProvider? _serviceProvider;

    /// <inheritdoc />
    public INinjadogEngineBuilder WithManifest(NinjadogTemplateManifest templateManifest)
    {
        _templateManifest = templateManifest;
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngineBuilder WithSettings(NinjadogSettings ninjadogSettings)
    {
        _ninjadogSettings = ninjadogSettings;
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngineBuilder WithOutputProcessors(NinjadogOutputProcessors outputProcessors)
    {
        _outputProcessors = outputProcessors;
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngineBuilder WithServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngine Build()
    {
        return _serviceProvider is null
            ? throw new InvalidOperationException("No service provider is set.")
            : new NinjadogEngine(
                EnsureIsSet(_templateManifest, "Template manifest"),
                EnsureIsSet(_ninjadogSettings, "Ninjadog settings"),
                EnsureIsSet(_outputProcessors, "Output processors"),
                _serviceProvider.GetRequiredService<INinjadogAppService>(),
                _serviceProvider.GetRequiredService<IDomainEventDispatcher>());
    }

    private static T EnsureIsSet<T>(T? value, string propertyName)
        where T : class
    {
        return value ?? throw new InvalidOperationException($"{propertyName} is not set.");
    }
}
