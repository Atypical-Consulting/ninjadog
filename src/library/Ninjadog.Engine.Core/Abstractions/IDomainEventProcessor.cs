// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Represents a handler for a specific type of domain event.
/// </summary>
/// <typeparam name="T">The type of the domain event to handle.</typeparam>
public interface IDomainEventProcessor<in T>
    where T : IDomainEvent
{
    /// <summary>
    /// Handles the given domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleAsync(T domainEvent);
}
