// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Config;
using Ninjadog.Settings.Entities;

namespace Ninjadog.Settings;

/// <summary>
/// Represents the settings for a Ninjadog Engine instance, encapsulating all necessary configurations.
/// This record includes both application-level configurations and entity definitions,
/// enabling tailored behavior of the Ninjadog Engine based on these settings.
/// </summary>
public record NinjadogSettings
{
    /// <summary>
    /// Gets the general configuration settings for the Ninjadog Engine.
    /// This includes parameters like root namespace, output paths, and other global settings.
    /// </summary>
    public virtual NinjadogConfiguration? Config { get; init; }

    /// <summary>
    /// Gets the collection of entities that the Ninjadog Engine will use for template generation.
    /// This encompasses the definitions and properties of all entities involved in the templating process.
    /// </summary>
    public virtual NinjadogEntities? Entities { get; init; }
}
