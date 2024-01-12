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

    public INinjadogEngineBuilder WithManifest(NinjadogTemplateManifest templateManifest)
    {
        _templateManifest = templateManifest;
        return this;
    }

    public INinjadogEngineBuilder WithSettings(NinjadogSettings ninjadogSettings)
    {
        _ninjadogSettings = ninjadogSettings;
        return this;
    }

    public INinjadogEngineBuilder AddOutputProcessor(IOutputProcessor outputProcessor)
    {
        _outputProcessors.Add(outputProcessor);
        return this;
    }

    public INinjadogEngine Build()
    {
        if (_templateManifest is null)
        {
            throw new InvalidOperationException("Template manifest is not set.");
        }

        if (_ninjadogSettings is null)
        {
            throw new InvalidOperationException("Ninjadog settings are not set.");
        }

        if (_outputProcessors.Count == 0)
        {
            throw new InvalidOperationException("No output processors are set.");
        }

        return new NinjadogEngine(
            _templateManifest,
            _ninjadogSettings,
            _outputProcessors);
    }
}
