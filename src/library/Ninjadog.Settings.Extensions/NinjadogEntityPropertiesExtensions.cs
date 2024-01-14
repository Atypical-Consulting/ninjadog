// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities.Properties;

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Provides extension methods for <see cref="NinjadogEntityProperties"/> collections.
/// These methods enhance the functionality of <see cref="NinjadogEntityProperties"/>, offering additional utilities
/// such as conversion and manipulation of entity properties within the collection.
/// </summary>
public static class NinjadogEntityPropertiesExtensions
{
    /// <summary>
    /// Converts the <see cref="NinjadogEntityProperties"/> collection into a list of <see cref="NinjadogEntityPropertyWithKey"/>.
    /// This method facilitates the use of entity properties by associating each with its corresponding key,
    /// thus providing a convenient way to handle entity properties with their identifying keys.
    /// </summary>
    /// <param name="properties">The <see cref="NinjadogEntityProperties"/> collection to be converted.</param>
    /// <returns>A list of <see cref="NinjadogEntityPropertyWithKey"/>, each representing a property along with its key.</returns>
    public static List<NinjadogEntityPropertyWithKey> FromKeys(
        this NinjadogEntityProperties properties)
    {
        return properties
            .Select(x => new NinjadogEntityPropertyWithKey(x.Key, x.Value))
            .ToList();
    }
}
