// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
    private readonly NinjadogOutputProcessors _outputProcessors = [];
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
    public INinjadogEngineBuilder WithOutputProcessor(IOutputProcessor outputProcessor)
    {
        _outputProcessors.Add(outputProcessor);
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngineBuilder WithOutputProcessors(List<IOutputProcessor> outputProcessors)
    {
        _outputProcessors.AddRange(outputProcessors);
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
                EnsureTemplateManifestIsSet(),
                EnsureNinjadogSettingsAreSet(),
                EnsureOutputProcessorsAreSet(),
                _serviceProvider.GetRequiredService<IFileService>(),
                _serviceProvider.GetRequiredService<IDomainEventDispatcher>());
    }

    private NinjadogTemplateManifest EnsureTemplateManifestIsSet()
    {
        return _templateManifest
               ?? throw new InvalidOperationException("Template manifest is not set.");
    }

    private NinjadogSettings EnsureNinjadogSettingsAreSet()
    {
        return _ninjadogSettings
               ?? throw new InvalidOperationException("Ninjadog settings are not set.");
    }

    private NinjadogOutputProcessors EnsureOutputProcessorsAreSet()
    {
        return _outputProcessors
               ?? throw new InvalidOperationException("No output processors are set.");
    }
}
