// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;
using Ninjadog.Settings;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Represents an event that is triggered after the engine has finished processing.
/// </summary>
/// <param name="Settings">The settings that were used for processing.</param>
/// <param name="Elapsed">The elapsed time for processing.</param>
/// <param name="TotalFilesGenerated">The total number of files generated.</param>
/// <param name="TotalCharactersGenerated">The total number of characters generated.</param>
public record AfterEngineRunEvent(
    NinjadogSettings Settings,
    TimeSpan Elapsed,
    int TotalFilesGenerated,
    int TotalCharactersGenerated) : DomainEvent;
