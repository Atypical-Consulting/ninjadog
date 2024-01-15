// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.EventArgs;

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for the Ninjadog Engine, which handles the execution of template generation processes.
/// This interface encapsulates the core functionality of running the templating engine to produce output based on specified templates.
/// </summary>
public interface INinjadogEngine
    : INinjadogEngineLifecycle, INinjadogEngineTemplateProcessing, INinjadogEngineContentProcessing, INinjadogEngineErrorHandling
{
    /// <summary>
    /// Occurs when the .NET CLI version is checked by the Ninjadog Engine.
    /// </summary>
    event EventHandler<Version>? OnDotnetVersionChecked;

    /// <summary>
    /// Occurs when the Ninjadog Engine has completed its run.
    /// </summary>
    event EventHandler<NinjadogEngineRunEventArgs>? OnRunCompleted;

    /// <summary>
    /// Runs the Ninjadog Engine to process and generate templates.
    /// This method triggers the execution of the template generation logic encapsulated by the engine.
    /// </summary>
    void Run();
}
