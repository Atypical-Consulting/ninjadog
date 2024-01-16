// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;
using Ninjadog.Engine.Core.Models;
using Ninjadog.Engine.OutputProcessors;
using Ninjadog.Settings;

namespace Ninjadog.Engine;

public sealed class NinjadogEngineBuilder
    : INinjadogEngineBuilder
{
    private NinjadogTemplateManifest? _templateManifest;
    private NinjadogSettings? _ninjadogSettings;
    private readonly OutputProcessorCollection _outputProcessors = [];
    private IDotnetCliService? _dotnetCommandService;
    private IFileService? _fileService;

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
    public INinjadogEngineBuilder WithDotnetCommandService(IDotnetCliService dotnetCliService)
    {
        _dotnetCommandService = dotnetCliService;
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngineBuilder WithFileService(IFileService fileService)
    {
        _fileService = fileService;
        return this;
    }

    /// <inheritdoc />
    public INinjadogEngine Build()
    {
        return new NinjadogEngine(
            EnsureTemplateManifestIsSet(),
            EnsureNinjadogSettingsAreSet(),
            EnsureOutputProcessorsAreSet(),
            EnsureDotnetCommandServiceIsSet(),
            EnsureFileServiceIsSet());
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

    private IDotnetCliService EnsureDotnetCommandServiceIsSet()
    {
        return _dotnetCommandService
               ?? throw new InvalidOperationException("No dotnet command service is set.");
    }

    private IFileService EnsureFileServiceIsSet()
    {
        return _fileService
               ?? throw new InvalidOperationException("No file service is set.");
    }
}
