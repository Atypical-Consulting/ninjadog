// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessTemplate;

/// <summary>
/// Represents an event that is triggered after a template has been parsed.
/// </summary>
/// <param name="Template">The template that has been parsed.</param>
public record AfterTemplateParsedEvent(NinjadogTemplate Template) : DomainEvent;
