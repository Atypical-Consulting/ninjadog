// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.EventArgs;

/// <summary>
/// Provides data for the RunCompleted event of NinjadogEngine.
/// </summary>
public class NinjadogEngineRunEventArgs
    : System.EventArgs
{
    /// <summary>
    /// Gets the duration of the run operation.
    /// </summary>
    public required TimeSpan RunTime { get; init; }

    /// <summary>
    /// Gets the total number of files generated.
    /// </summary>
    public required int TotalFilesGenerated { get; init; }

    /// <summary>
    /// Gets the total number of characters generated.
    /// </summary>
    public required int TotalCharactersGenerated { get; init; }

    /// <summary>
    /// Gets the exceptions that were encountered during the run operation.
    /// </summary>
    public required List<Exception> Exceptions { get; init; }
}
