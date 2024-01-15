// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;
using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.Collections;
using Ninjadog.Engine.EventArgs;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Internals;

internal sealed class NinjadogEngine : INinjadogEngine
{
    // Fields
    private readonly NinjadogTemplateManifest _templateManifest;
    private readonly NinjadogSettings _ninjadogSettings;
    private readonly OutputProcessorCollection _outputProcessors;
    private readonly IDotnetCommandService _dotnetCommandService;

    private int _totalFilesGenerated;
    private int _totalCharactersGenerated;
    private readonly List<Exception> _exceptions = [];

    public NinjadogEngine(
        NinjadogTemplateManifest templateManifest,
        NinjadogSettings ninjadogSettings,
        OutputProcessorCollection outputProcessors,
        IDotnetCommandService dotnetCommandService)
    {
        _templateManifest = templateManifest ?? throw new ArgumentNullException(nameof(templateManifest));
        _ninjadogSettings = ninjadogSettings ?? throw new ArgumentNullException(nameof(ninjadogSettings));
        _outputProcessors = outputProcessors ?? throw new ArgumentNullException(nameof(outputProcessors));
        _dotnetCommandService = dotnetCommandService ?? throw new ArgumentNullException(nameof(dotnetCommandService));

        HandleInitialized();
    }

    // Events
    public event EventHandler<Version>? OnDotnetVersionChecked;
    public event EventHandler<NinjadogEngineRunEventArgs>? OnRunCompleted;
    public event EventHandler<NinjadogTemplateEventArgs>? OnBeforeTemplateProcessed;
    public event EventHandler<NinjadogTemplateEventArgs>? OnAfterTemplateProcessed;
    public event EventHandler<NinjadogErrorEventArgs>? OnErrorOccurred;
    public event EventHandler<NinjadogContentEventArgs>? OnBeforeContentProcessed;
    public event EventHandler<NinjadogContentEventArgs>? OnAfterContentProcessed;
    public event EventHandler? OnInitialized;
    public event EventHandler? OnShutdown;

    public void Run()
    {
        Reset();
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // ensure the .NET CLI is available
            var version = _dotnetCommandService.Version();
            HandleDotnetVersionChecked(version);

            // run the engine for each template in the manifest
            foreach (var template in _templateManifest.Templates)
            {
                ProcessTemplate(template);
            }
        }
        catch (Exception ex)
        {
            HandleErrorOccurred(ex);
        }
        finally
        {
            stopwatch.Stop();
            HandleRunCompleted(stopwatch.Elapsed, _totalFilesGenerated, _totalCharactersGenerated, _exceptions);
            HandleShutdown();
        }
    }

    private void Reset()
    {
        _totalFilesGenerated = 0;
        _totalCharactersGenerated = 0;
        _exceptions.Clear();
    }

    private void ProcessTemplate(NinjadogTemplate template)
    {
        HandleBeforeTemplateProcessed(template);

        try
        {
            // First, add a single file based on the template and the settings...
            var singleFileContent = template.GenerateOne(_ninjadogSettings);
            ProcessContent(singleFileContent);

            // ...then, add multiple files based on the template and the settings
            foreach (var content in template.GenerateMany(_ninjadogSettings))
            {
                ProcessContent(content);
            }
        }
        catch (Exception ex)
        {
            HandleErrorOccurred(ex);
        }
        finally
        {
            HandleAfterTemplateProcessed(template);
        }
    }

    private void ProcessContent(NinjadogContentFile? contentFile)
    {
        if (contentFile is null || string.IsNullOrEmpty(contentFile.Content))
        {
            return;
        }

        HandleBeforeContentProcessed(contentFile);

        foreach (var processor in _outputProcessors)
        {
            processor.ProcessOutput(contentFile);
        }

        HandleAfterContentProcessed(contentFile);
    }

    private void HandleBeforeTemplateProcessed(NinjadogTemplate template)
    {
        SafeInvokeEvent(() => OnBeforeTemplateProcessed?.Invoke(this,
            new NinjadogTemplateEventArgs { Template = template }));
    }

    private void HandleAfterTemplateProcessed(NinjadogTemplate template)
    {
        SafeInvokeEvent(() => OnAfterTemplateProcessed?.Invoke(this,
            new NinjadogTemplateEventArgs { Template = template }));
    }

    private void HandleBeforeContentProcessed(NinjadogContentFile contentFile)
    {
        SafeInvokeEvent(() => OnBeforeContentProcessed?.Invoke(this,
            new NinjadogContentEventArgs { ContentFile = contentFile }));
    }

    private void HandleAfterContentProcessed(NinjadogContentFile contentFile)
    {
        _totalFilesGenerated++;
        _totalCharactersGenerated += contentFile.Length;
        SafeInvokeEvent(() => OnAfterContentProcessed?.Invoke(this,
            new NinjadogContentEventArgs { ContentFile = contentFile }));
    }

    private void HandleErrorOccurred(Exception exception)
    {
        _exceptions.Add(exception);
        SafeInvokeEvent(() => OnErrorOccurred?.Invoke(this,
            new NinjadogErrorEventArgs { Exception = exception }));
    }

    private void HandleInitialized()
    {
        SafeInvokeEvent(() => OnInitialized?.Invoke(this,
            System.EventArgs.Empty));
    }

    private void HandleShutdown()
    {
        SafeInvokeEvent(() => OnShutdown?.Invoke(this,
            System.EventArgs.Empty));
    }

    private void HandleDotnetVersionChecked(string? version)
    {
        SafeInvokeEvent(() => OnDotnetVersionChecked?.Invoke(this,
            Version.Parse(version ?? "0.0.0")));
    }

    private void HandleRunCompleted(
        TimeSpan runTime,
        int totalFilesGenerated,
        int totalCharactersGenerated,
        List<Exception> exceptions)
    {
        SafeInvokeEvent(() => OnRunCompleted?.Invoke(this,
            new NinjadogEngineRunEventArgs
            {
                RunTime = runTime,
                TotalFilesGenerated = totalFilesGenerated,
                TotalCharactersGenerated = totalCharactersGenerated,
                Exceptions = exceptions
            }));
    }

    private static void SafeInvokeEvent(Action? eventAction)
    {
        try
        {
            eventAction?.Invoke();
        }
        catch (Exception _)
        {
            // Log the exception or handle it as necessary
        }
    }
}
