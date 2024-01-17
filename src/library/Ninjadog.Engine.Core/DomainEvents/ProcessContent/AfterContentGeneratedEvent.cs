// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessContent;

/// <summary>
/// Represents an event that is triggered after content has been generated.
/// </summary>
/// <param name="ContentFile">The content file that has been generated.</param>
public record AfterContentGeneratedEvent(NinjadogContentFile ContentFile) : DomainEvent;
