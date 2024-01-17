// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessContent;

/// <summary>
/// Handles events that occur after content is generated.
/// </summary>
public class AfterContentGeneratedProcessor
    : IDomainEventProcessor<AfterContentGeneratedEvent>
{
    /// <summary>
    /// Handles the logic to be executed after content has been generated.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the generated content.</param>
    public async Task HandleAsync(AfterContentGeneratedEvent domainEvent)
    {
        // Logic for handling the event after content is generated
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
