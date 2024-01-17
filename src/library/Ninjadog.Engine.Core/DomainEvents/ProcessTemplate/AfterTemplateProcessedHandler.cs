// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Handles events that occur after a template is processed.
/// </summary>
public class AfterTemplateProcessedHandler
    : IDomainEventHandler<AfterTemplateProcessedEvent>
{
    /// <summary>
    /// Handles the logic to be executed after a template has been processed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the processed template.</param>
    public async Task HandleAsync(AfterTemplateProcessedEvent domainEvent)
    {
        // Logic for handling the event after a template is processed
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
