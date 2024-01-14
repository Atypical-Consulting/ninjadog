// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.Collections;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Internals;

internal sealed class NinjadogEngineBuilder
    : INinjadogEngineBuilder
{
    private NinjadogTemplateManifest? _templateManifest;
    private NinjadogSettings? _ninjadogSettings;
    private readonly OutputProcessorCollection _outputProcessors = [];

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
    public INinjadogEngineBuilder AddOutputProcessor(IOutputProcessor outputProcessor)
    {
        _outputProcessors.Add(outputProcessor);
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngine Build()
    {
        return new NinjadogEngine(
            EnsureTemplateManifestIsSet(),
            EnsureNinjadogSettingsAreSet(),
            EnsureOutputProcessorsAreSet());
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

    private OutputProcessorCollection EnsureOutputProcessorsAreSet()
    {
        return _outputProcessors
               ?? throw new InvalidOperationException("No output processors are set.");
    }
}
