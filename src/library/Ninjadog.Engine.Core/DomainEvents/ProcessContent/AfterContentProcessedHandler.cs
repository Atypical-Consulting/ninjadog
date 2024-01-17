// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Handles events that occur after content is processed.
/// </summary>
public class AfterContentProcessedHandler
    : IDomainEventHandler<AfterContentProcessedEvent>
{
    /// <summary>
    /// Handles the logic to be executed after content has been processed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the processed content.</param>
    public async Task HandleAsync(AfterContentProcessedEvent domainEvent)
    {
        var contentFile = domainEvent.ContentFile;
        // Logic for handling the event after content is processed
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
