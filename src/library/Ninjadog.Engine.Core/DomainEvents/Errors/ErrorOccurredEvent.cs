// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Core.DomainEvents.Errors;

/// <summary>
/// Represents an event that is triggered when an error occurs.
/// </summary>
/// <param name="Settings">The settings that will be used for processing.</param>
/// <param name="TemplateManifest">The template manifest that will be used for processing.</param>
/// <param name="ContextSnapshot">The context snapshot that will be used for processing.</param>
/// <param name="Exception">The exception that occurred.</param>
public record ErrorOccurredEvent(
    NinjadogSettings Settings,
    NinjadogTemplateManifest TemplateManifest,
    NinjadogEngineContextSnapshot ContextSnapshot,
    Exception Exception)
    : NinjadogEngineEvent(Settings, TemplateManifest, ContextSnapshot);
