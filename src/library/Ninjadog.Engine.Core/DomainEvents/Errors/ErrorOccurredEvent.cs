// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.DomainEvents.Errors;

/// <summary>
/// Represents an event that is triggered when an error occurs.
/// </summary>
/// <param name="Exception">The exception that occurred.</param>
public record ErrorOccurredEvent(Exception Exception) : DomainEvent;
