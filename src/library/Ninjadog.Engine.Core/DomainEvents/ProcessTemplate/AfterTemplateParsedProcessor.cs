// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
    public async Task HandleAsync(AfterTemplateParsedEvent domainEvent)
    {
        // Logic for handling the event after a template is generated
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
