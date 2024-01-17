// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
    public async Task HandleAsync(ErrorOccurredEvent domainEvent)
    {
        // Logic for handling the event when an error occurs
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
