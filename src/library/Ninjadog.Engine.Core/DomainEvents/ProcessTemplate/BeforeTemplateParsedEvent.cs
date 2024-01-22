// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Core.DomainEvents.ProcessTemplate;

/// <summary>
/// Represents an event that is triggered before a template is parsed.
/// </summary>
/// <param name="Settings">The settings that will be used for processing.</param>
/// <param name="TemplateManifest">The template manifest that will be used for processing.</param>
/// <param name="ContextSnapshot">The context snapshot that will be used for processing.</param>
/// <param name="Template">The template that is about to be parsed.</param>
public record BeforeTemplateParsedEvent(
    NinjadogSettings Settings,
    NinjadogTemplateManifest TemplateManifest,
    NinjadogEngineContextSnapshot ContextSnapshot,
    NinjadogTemplate Template)
    : NinjadogEngineEvent(Settings, TemplateManifest, ContextSnapshot);
