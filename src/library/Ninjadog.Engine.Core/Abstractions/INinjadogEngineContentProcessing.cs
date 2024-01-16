// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.EventArgs;

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Defines the interface for handling content processing events in the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineContentProcessing
{
    /// <summary>
    /// Occurs before a content file is processed by the Ninjadog Engine.
    /// This event can be used to inspect or modify the content file before it undergoes further processing.
    /// </summary>
    event EventHandler<NinjadogContentEventArgs>? OnBeforeContentProcessed;

    /// <summary>
    /// Occurs after a content file has been processed by the Ninjadog Engine.
    /// This event allows for actions to be taken after a content file has been fully processed, such as additional formatting or logging.
    /// </summary>
    event EventHandler<NinjadogContentEventArgs>? OnAfterContentProcessed;
}
