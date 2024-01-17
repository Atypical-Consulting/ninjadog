// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.Run;

/// <summary>
/// Handles events that occur after the engine has finished processing.
/// </summary>
public class AfterEngineRunProcessor
    : IDomainEventProcessor<AfterEngineRunEvent>
{
    /// <summary>
    /// Handles the logic to be executed when the engine has finished processing.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the engine settings.</param>
    public async Task HandleAsync(AfterEngineRunEvent domainEvent)
    {
        // Logic for handling the event before the engine starts processing
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
