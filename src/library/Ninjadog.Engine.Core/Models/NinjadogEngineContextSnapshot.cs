// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Represents a snapshot of the NinjadogEngineContext.
/// </summary>
/// <param name="TotalFilesGenerated"></param>
/// <param name="TotalCharactersGenerated"></param>
/// <param name="TotalTimeElapsed"></param>
public record NinjadogEngineContextSnapshot(
    int TotalFilesGenerated,
    int TotalCharactersGenerated,
    TimeSpan TotalTimeElapsed)
{
    // MarkupLine($"  - It represents ~[green]{totalCharactersGenerated / 5}[/] words or ~[green]{totalCharactersGenerated / 150}[/] minutes saved");

    /// <summary>
    /// Gets the total number of words generated.
    /// </summary>
    /// <remarks>
    /// A word is considered to be 5 characters long.
    /// </remarks>
    /// <returns>The total number of words generated.</returns>
    public int TotalWordsGenerated => TotalCharactersGenerated / 5;

    /// <summary>
    /// Gets the total number of minutes saved by using Ninjadog.
    /// </summary>
    /// <returns>The total number of minutes saved.</returns>
    public int TotalMinutesSaved => TotalCharactersGenerated / 150;
}
