// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;
using Ninjadog.Engine.Core.Abstractions;
using Ninjadog.Engine.Core.EventArgs;
using Ninjadog.Engine.Core.Models;
using Ninjadog.Engine.OutputProcessors;
using Ninjadog.Settings;

namespace Ninjadog.Engine;

public sealed class NinjadogEngine : INinjadogEngine
{
    // Fields
    private readonly NinjadogTemplateManifest _templateManifest;
    private readonly NinjadogSettings _ninjadogSettings;
    private readonly OutputProcessorCollection _outputProcessors;
    private readonly IDotnetCliService _dotnetCliService;
    private readonly IFileService _fileService;

    private int _totalFilesGenerated;
    private int _totalCharactersGenerated;
    private readonly List<Exception> _exceptions = [];

    public NinjadogEngine(
        NinjadogTemplateManifest templateManifest,
        NinjadogSettings ninjadogSettings,
        OutputProcessorCollection outputProcessors,
        IDotnetCliService dotnetCliService,
        IFileService fileService)
    {
        _templateManifest = templateManifest ?? throw new ArgumentNullException(nameof(templateManifest));
        _ninjadogSettings = ninjadogSettings ?? throw new ArgumentNullException(nameof(ninjadogSettings));
        _outputProcessors = outputProcessors ?? throw new ArgumentNullException(nameof(outputProcessors));
        _dotnetCliService = dotnetCliService ?? throw new ArgumentNullException(nameof(dotnetCliService));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));

        HandleInitialized();
    }

    // Events
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
            // delete the app folder if it already exists and create it again
            var appName = _ninjadogSettings.Config.Name;
            _fileService.DeleteAppFolder(appName);
            var appDirectory = _fileService.CreateAppFolder(appName);

            // create the .net solution with the app name
            var dotnetVersion = _dotnetCliService.Version();
            var createSlnResult = _dotnetCliService.CreateSolution(appDirectory);
            var buildResult = _dotnetCliService.Build(appDirectory);

            // Install NuGet packages
            foreach (var package in _templateManifest.NuGetPackages)
            {
                _dotnetCliService.AddPackage(appDirectory, package);
            }

            // create the template folder
            var templateName = _templateManifest.Name;
            _fileService.CreateSubFolder(appName, templateName);

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
            HandleRunCompleted(stopwatch.Elapsed);
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
        NinjadogTemplateEventArgs args = new() { Template = template };
        OnBeforeTemplateProcessed?.SafeInvokeEvent(this, args);
    }

    private void HandleAfterTemplateProcessed(NinjadogTemplate template)
    {
        NinjadogTemplateEventArgs args = new() { Template = template };
        OnAfterTemplateProcessed?.SafeInvokeEvent(this, args);
    }

    private void HandleBeforeContentProcessed(NinjadogContentFile contentFile)
    {
        NinjadogContentEventArgs args = new() { ContentFile = contentFile };
        OnBeforeContentProcessed?.SafeInvokeEvent(this, args);
    }

    private void HandleAfterContentProcessed(NinjadogContentFile contentFile)
    {
        _totalFilesGenerated++;
        _totalCharactersGenerated += contentFile.Length;

        // TODO: use the file service in the appropriate output processor
        var appName = _ninjadogSettings.Config.Name;
        var templateName = _templateManifest.Name;
        var path = Path.Combine(appName, templateName, contentFile.OutputPath);
        _fileService.CreateFile(path, contentFile.Content);

        var args = new NinjadogContentEventArgs { ContentFile = contentFile };
        OnAfterContentProcessed?.SafeInvokeEvent(this, args);
    }

    private void HandleErrorOccurred(Exception exception)
    {
        _exceptions.Add(exception);

        var args = new NinjadogErrorEventArgs { Exception = exception };
        OnErrorOccurred?.SafeInvokeEvent(this, args);
    }

    private void HandleInitialized()
    {
        OnInitialized?.SafeInvokeEvent(this);
    }

    private void HandleShutdown()
    {
        OnShutdown?.SafeInvokeEvent(this);
    }

    private void HandleRunCompleted(TimeSpan runTime)
    {
        var args = new NinjadogEngineRunEventArgs
        {
            RunTime = runTime,
            TotalFilesGenerated = _totalFilesGenerated,
            TotalCharactersGenerated = _totalCharactersGenerated,
            Exceptions = _exceptions
        };

        OnRunCompleted?.SafeInvokeEvent(this, args);
    }
}
