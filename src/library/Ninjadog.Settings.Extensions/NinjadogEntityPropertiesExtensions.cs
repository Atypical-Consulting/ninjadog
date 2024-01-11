// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Settings.Extensions;

public static class NinjadogEntityPropertiesExtensions
{
    public static List<NinjadogEntityPropertyWithKey> FromKeys(
        this NinjadogEntityProperties properties)
    {
        return properties
            .Select(x => new NinjadogEntityPropertyWithKey(x.Key, x.Value))
            .ToList();
    }
}
