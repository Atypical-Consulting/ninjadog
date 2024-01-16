// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Defines the interface for the lifecycle of the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineLifecycle
{
    /// <summary>
    /// Occurs when the Ninjadog Engine is initialized and ready to start processing.
    /// This event can be used to perform any setup or initialization tasks before the engine begins its operations.
    /// </summary>
    event EventHandler? OnInitialized;

    /// <summary>
    /// Occurs when the Ninjadog Engine has completed all its operations and is shutting down.
    /// This event is useful for cleanup, releasing resources, or finalizing activities post-execution.
    /// </summary>
    event EventHandler? OnShutdown;
}
