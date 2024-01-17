// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Represents an event that is triggered after a template has been processed.
/// </summary>
/// <param name="Template">The template that has been processed.</param>
public record AfterTemplateProcessedEvent(NinjadogTemplate Template) : DomainEvent;
