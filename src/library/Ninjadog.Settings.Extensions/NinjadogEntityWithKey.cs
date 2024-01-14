// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities;
using Ninjadog.Settings.Entities.Properties;

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Represents a Ninjadog entity along with its key.
/// This record extends <see cref="NinjadogEntity"/> by associating it with a specific key,
/// enabling more specific template generation based on the key-value relationship.
/// </summary>
/// <param name="Key">The key of the entity.</param>
/// <param name="Properties">The properties of the entity.</param>
public sealed record NinjadogEntityWithKey(
    string Key,
    NinjadogEntityProperties Properties)
    : NinjadogEntity(Properties)
{
    /// <summary>
    /// Gets the string tokens generated from the entity key.
    /// </summary>
    public StringTokens StringTokens => new(Key);

    /// <summary>
    /// Generates the member properties for the entity.
    /// This method produces C# code for each property within the entity, formatted as member definitions.
    /// </summary>
    /// <returns>A <see cref="string"/> containing the C# code for all member properties of the entity.</returns>
    public string GenerateMemberProperties()
    {
        return Properties
            .FromKeys()
            .Select(p => p.GenerateMemberProperty())
            .Aggregate((x, y) => $"{x}\n{y}");
    }
}
