// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessContent;

/// <summary>
/// Handles events that occur before content is generated.
/// </summary>
public class BeforeContentGeneratedProcessor
    : IDomainEventProcessor<BeforeContentGeneratedEvent>
{
    /// <summary>
    /// Handles the logic to be executed when content is about to be generated.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the content to be generated.</param>
    public async Task HandleAsync(BeforeContentGeneratedEvent domainEvent)
    {
        // Logic for handling the event before content is generated
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
