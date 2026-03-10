// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

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
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task HandleAsync(AfterEngineRunEvent domainEvent)
    {
        // Logic for handling the event before the engine starts processing
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
