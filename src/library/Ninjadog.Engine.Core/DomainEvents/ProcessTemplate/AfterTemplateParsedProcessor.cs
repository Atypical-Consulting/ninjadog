// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessTemplate;

/// <summary>
/// Handles events that occur after a template is parsed.
/// </summary>
public class AfterTemplateParsedProcessor
    : IDomainEventProcessor<AfterTemplateParsedEvent>
{
    /// <summary>
    /// Handles the logic to be executed after a template has been parsed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the parsed template.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task HandleAsync(AfterTemplateParsedEvent domainEvent)
    {
        // Logic for handling the event after a template is generated
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
