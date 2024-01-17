// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Handles events that occur before a template is processed.
/// </summary>
public class BeforeTemplateProcessedHandler
    : IDomainEventHandler<BeforeTemplateProcessedEvent>
{
    /// <summary>
    /// Handles the logic to be executed when a template is about to be processed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the template to be processed.</param>
    public async Task HandleAsync(BeforeTemplateProcessedEvent domainEvent)
    {
        // Logic for handling the event before a template is processed
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
