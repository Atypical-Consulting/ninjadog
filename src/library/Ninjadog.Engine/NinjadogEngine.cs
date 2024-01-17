// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;

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
/// <param name="fileService">The file service to be used by the engine.</param>
/// <param name="domainEventDispatcher">The domain event dispatcher to be used by the engine.</param>
/// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
public sealed class NinjadogEngine(
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings,
    NinjadogOutputProcessors outputProcessors,
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

            foreach (var template in templateManifest.Templates)
            {
                ProcessTemplate(template);
            }
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

    private void ProcessTemplate(NinjadogTemplate template)
    {
        try
        {
            DispatchBeforeEngineTemplateGenerated(template);

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
            DispatchAfterTemplateGenerated(template);
        }
    }

    private void ProcessContent(NinjadogContentFile? contentFile)
    {
        if (contentFile is null || string.IsNullOrEmpty(contentFile.Content))
        {
            return;
        }

        DispatchBeforeContentGenerated(contentFile);

        foreach (var processor in outputProcessors)
        {
            processor.ProcessOutput(contentFile);
        }

        DispatchAfterContentGenerated(contentFile);
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
        _stopwatch?.Stop();

        AfterEngineRunEvent domainEvent = new(
            ninjadogSettings,
            _stopwatch?.Elapsed ?? TimeSpan.Zero,
            _totalFilesGenerated,
            _totalCharactersGenerated);

        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchBeforeEngineTemplateGenerated(NinjadogTemplate template)
    {
        var domainEvent = new BeforeTemplateParsedEvent(template);
        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchAfterTemplateGenerated(NinjadogTemplate template)
    {
        var domainEvent = new AfterTemplateParsedEvent(template);
        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchBeforeContentGenerated(NinjadogContentFile contentFile)
    {
        var domainEvent = new BeforeContentGeneratedEvent(contentFile);
        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchAfterContentGenerated(NinjadogContentFile contentFile)
    {
        _totalFilesGenerated++;
        _totalCharactersGenerated += contentFile.Length;

        // TODO: use the file service in the appropriate output processor
        var appName = ninjadogSettings.Config.Name;
        var templateName = templateManifest.Name;
        var path = Path.Combine(appName, templateName, contentFile.OutputPath);
        fileService.CreateFile(path, contentFile.Content);

        var domainEvent = new AfterContentGeneratedEvent(contentFile);
        domainEventDispatcher.Dispatch(domainEvent);
    }

    private void DispatchError(Exception ex)
    {
        domainEventDispatcher.Dispatch(new ErrorOccurredEvent(ex));
    }
}
