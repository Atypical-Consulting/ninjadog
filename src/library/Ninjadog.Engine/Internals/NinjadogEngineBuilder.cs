// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.Collections;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Internals;

internal class NinjadogEngineBuilder
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
