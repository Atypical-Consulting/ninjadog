// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessTemplate;

/// <summary>
/// Handles events that occur before a template is parsed.
/// </summary>
public class BeforeTemplateParsedProcessor
    : IDomainEventProcessor<BeforeTemplateParsedEvent>
{
    /// <summary>
    /// Handles the logic to be executed when a template is about to be parsed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the template to be parsed.</param>
    public async Task HandleAsync(BeforeTemplateParsedEvent domainEvent)
    {
        // Logic for handling the event before a template is parsed
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
