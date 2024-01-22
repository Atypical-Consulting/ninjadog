// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Core.DomainEvents.Errors;

/// <summary>
/// Handles events that occur when an error occurs.
/// </summary>
public class ErrorOccurredProcessor
    : IDomainEventProcessor<ErrorOccurredEvent>
{
    /// <summary>
    /// Handles the logic to be executed when an error occurs.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the error.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task HandleAsync(ErrorOccurredEvent domainEvent)
    {
        // Logic for handling the event when an error occurs
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
