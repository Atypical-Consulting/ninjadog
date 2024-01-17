// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;
using Ninjadog.Engine.Core.DomainEvents;

namespace Ninjadog.Engine;

/// <summary>
/// Represents the Ninjadog Engine, which handles the execution of template generation processes.
/// This class encapsulates the core functionality of running the templating engine to produce output based on specified templates.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NinjadogEngine"/> class.
/// </remarks>
/// <param name="templateManifest">The template manifest to be used by the engine.</param>
/// <param name="ninjadogSettings">The ninjadog app settings to configure the engine.</param>
/// <param name="outputProcessors">The output processors to be used by the engine.</param>
/// <param name="cliDotnetService">The dotnet command service to be used by the engine.</param>
/// <param name="fileService">The file service to be used by the engine.</param>
/// <param name="domainEventDispatcher">The domain event dispatcher to be used by the engine.</param>
/// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
public sealed class NinjadogEngine(
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings,
    NinjadogOutputProcessors outputProcessors,
    ICliDotnetService cliDotnetService,
    IFileService fileService,
    IDomainEventDispatcher domainEventDispatcher)
    : INinjadogEngine
{
    private int _totalFilesGenerated;
    private int _totalCharactersGenerated;
    private Stopwatch? _stopwatch;

    /// <inheritdoc />
    public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();

    /// <inheritdoc />
    public void Run()
    {
        try
        {
            Reset();
            DispatchBeforeEngineRun();

            // delete the app folder if it already exists and create it again
            var appName = ninjadogSettings.Config.Name;
            fileService.DeleteAppFolder(appName);
            var appDirectory = fileService.CreateAppFolder(appName);

            // create the .net solution with the app name
            var dotnetVersion = cliDotnetService.Version();
            var createSlnResult = cliDotnetService.CreateSolution(appDirectory);
            var buildResult = cliDotnetService.Build(appDirectory);

            // throw new InvalidOperationException("Just because. :)");

            // Install NuGet packages
            foreach (var package in templateManifest.NuGetPackages)
            {
                cliDotnetService.AddPackage(appDirectory, package);
            }

            // create the template folder
            var templateName = templateManifest.Name;
            fileService.CreateSubFolder(appName, templateName);

            // run the engine for each template in the manifest
            ProcessTemplates();
        }
        catch (Exception ex)
        {
            DispatchError(ex);
        }
        finally
        {
            DispatchAfterEngineRun();
        }
    }

    private void Reset()
    {
        _totalFilesGenerated = 0;
        _totalCharactersGenerated = 0;
        _stopwatch = Stopwatch.StartNew();
    }

    private void ProcessTemplates()
    {
        foreach (var template in templateManifest.Templates)
        {
            ProcessTemplate(template);
        }
    }

    private void ProcessTemplate(NinjadogTemplate template)
    {
        try
        {
            DispatchBeforeEngineTemplateProcessed(template);

            // First, add a single file based on the template and the settings...
            var singleFileContent = template.GenerateOne(ninjadogSettings);
            ProcessContent(singleFileContent);

            // ...then, add multiple files based on the template and the settings
            foreach (var content in template.GenerateMany(ninjadogSettings))
            {
                ProcessContent(content);
            }
        }
        catch (Exception ex)
        {
            DispatchError(ex);
        }
        finally
        {
            DispatchAfterTemplateProcessed(template);
        }
    }

    private void ProcessContent(NinjadogContentFile? contentFile)
    {
        if (contentFile is null || string.IsNullOrEmpty(contentFile.Content))
        {
            return;
        }

        DispatchBeforeContentProcessed(contentFile);

        foreach (var processor in outputProcessors)
        {
            processor.ProcessOutput(contentFile);
        }

        DispatchAfterContentProcessed(contentFile);
    }

    //********************
    // Dispatcher methods
    //********************

    private void DispatchBeforeEngineRun()
    {
        var domainEvent = new BeforeEngineRunEvent(ninjadogSettings, templateManifest);
        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchAfterEngineRun()
    {
        _stopwatch.Stop();

        AfterEngineRunEvent domainEvent = new(
            ninjadogSettings,
            _stopwatch.Elapsed,
            _totalFilesGenerated,
            _totalCharactersGenerated);

        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchBeforeEngineTemplateProcessed(NinjadogTemplate template)
    {
        domainEventDispatcher.Dispatch(new BeforeTemplateProcessedEvent(template));
    }

    private void DispatchAfterTemplateProcessed(NinjadogTemplate template)
    {
        domainEventDispatcher.Dispatch(new AfterTemplateProcessedEvent(template));
    }

    private void DispatchBeforeContentProcessed(NinjadogContentFile contentFile)
    {
        domainEventDispatcher.Dispatch(new BeforeContentProcessedEvent(contentFile));
    }

    private void DispatchAfterContentProcessed(NinjadogContentFile contentFile)
    {
        _totalFilesGenerated++;
        _totalCharactersGenerated += contentFile.Length;

        // TODO: use the file service in the appropriate output processor
        var appName = ninjadogSettings.Config.Name;
        var templateName = templateManifest.Name;
        var path = Path.Combine(appName, templateName, contentFile.OutputPath);
        fileService.CreateFile(path, contentFile.Content);

        domainEventDispatcher.Dispatch(new AfterContentProcessedEvent(contentFile));
    }

    private void DispatchError(Exception ex)
    {
        domainEventDispatcher.Dispatch(new ErrorOccurredEvent(ex));
    }
}
