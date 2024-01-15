// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.EventArgs;

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for handling template processing events in the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineTemplateProcessing
{
    /// <summary>
    /// Occurs before a template is processed by the Ninjadog Engine.
    /// This event provides an opportunity to perform actions or modifications before each template processing begins.
    /// </summary>
    event EventHandler<NinjadogTemplateEventArgs>? OnBeforeTemplateProcessed;

    /// <summary>
    /// Occurs after a template has been processed by the Ninjadog Engine.
    /// This event is useful for post-processing actions or for gathering information after a template has been processed.
    /// </summary>
    event EventHandler<NinjadogTemplateEventArgs>? OnAfterTemplateProcessed;
}
