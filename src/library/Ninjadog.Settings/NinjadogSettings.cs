// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Text.Json;
using Ninjadog.Settings.Config;
using Ninjadog.Settings.Entities;

namespace Ninjadog.Settings;

/// <summary>
/// Represents the settings for a Ninjadog Engine instance, encapsulating all necessary configurations.
/// This record includes both application-level configurations and entity definitions,
/// enabling tailored behavior of the Ninjadog Engine based on these settings.
/// </summary>
/// <param name="Config">The general configuration settings for the Ninjadog Engine.</param>
/// <param name="Entities">The collection of entities that the Ninjadog Engine will use for template generation.</param>
public abstract record NinjadogSettings(
    NinjadogConfiguration Config,
    NinjadogEntities Entities)
{
    /// <summary>
    /// Serializes the record to JSON using the JsonSerializationContext.
    /// </summary>
    /// <returns>A JSON string representing the NinjadogSettings record.</returns>
    public string ToJsonString()
    {
        return JsonSerializer.Serialize(this, JsonSerializationContext.Default.NinjadogSettings);
    }
}
