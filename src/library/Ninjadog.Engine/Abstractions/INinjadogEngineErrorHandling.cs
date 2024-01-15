// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.EventArgs;

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for handling errors that occur during the execution of the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineErrorHandling
{
    /// <summary>
    /// Occurs when an error is encountered during the operation of the Ninjadog Engine.
    /// This event allows for error logging, analysis, or handling specific exceptions that occur during processing.
    /// </summary>
    event EventHandler<NinjadogErrorEventArgs>? OnErrorOccurred;
}
