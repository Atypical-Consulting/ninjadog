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
    TimeSpan TotalTimeElapsed);
