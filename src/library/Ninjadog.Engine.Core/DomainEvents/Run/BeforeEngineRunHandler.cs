// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.Run;

/// <summary>
/// Handles events that occur before the engine starts processing.
/// </summary>
/// <param name="fileService">The file service to be used by the handler.</param>
public class BeforeEngineRunProcessor(
    IFileService fileService,
    INinjadogAppService ninjadogAppService)
    : IDomainEventProcessor<BeforeEngineRunEvent>
{
    /// <summary>
    /// Handles the logic to be executed when the engine is about to start processing.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the engine settings.</param>
    public async Task HandleAsync(BeforeEngineRunEvent domainEvent)
    {
        var settings = domainEvent.Settings;
        var templateManifest = domainEvent.TemplateManifest;
        var appName = settings.Config.Name;

        // delete the app folder if it already exists
        fileService.DeleteAppFolder(appName);

        ninjadogAppService
            .Initialize(settings, templateManifest)
            .CreateApp();

        // create the template folder
        var templateName = templateManifest.Name;
        fileService.CreateSubFolder(appName, templateName);

        // Logic for handling the event before the engine starts processing
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
