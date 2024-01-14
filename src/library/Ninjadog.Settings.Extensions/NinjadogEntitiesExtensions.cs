// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities;

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Provides extension methods for <see cref="NinjadogEntities"/> collections.
/// These methods extend the functionality of <see cref="NinjadogEntities"/>, offering additional utilities
/// such as conversion and manipulation of entities within the collection.
/// </summary>
public static class NinjadogEntitiesExtensions
{
    /// <summary>
    /// Converts the <see cref="NinjadogEntities"/> collection into a list of <see cref="NinjadogEntityWithKey"/>.
    /// This method facilitates the use of entities by associating each with its corresponding key.
    /// </summary>
    /// <param name="entities">The <see cref="NinjadogEntities"/> collection to be converted.</param>
    /// <returns>A list of <see cref="NinjadogEntityWithKey"/>.</returns>
    public static List<NinjadogEntityWithKey> FromKeys(
        this NinjadogEntities entities)
    {
        return entities
            .Select(x => new NinjadogEntityWithKey(x.Key, x.Value.Properties))
            .ToList();
    }
}
