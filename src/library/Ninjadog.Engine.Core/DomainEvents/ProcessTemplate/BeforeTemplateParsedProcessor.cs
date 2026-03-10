// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessTemplate;

/// <summary>
/// Handles events that occur before a template is parsed.
/// </summary>
public class BeforeTemplateParsedProcessor
    : IDomainEventProcessor<BeforeTemplateParsedEvent>
{
    /// <summary>
    /// Handles the logic to be executed when a template is about to be parsed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the template to be parsed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task HandleAsync(BeforeTemplateParsedEvent domainEvent)
    {
        // Logic for handling the event before a template is parsed
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
