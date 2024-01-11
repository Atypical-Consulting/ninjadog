// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Settings.Extensions;

public static class NinjadogEntitiesExtensions
{
    public static List<NinjadogEntityWithKey> FromKeys(
        this NinjadogEntities entities)
    {
        return entities
            .Select(x => new NinjadogEntityWithKey(x.Key, x.Value.Properties))
            .ToList();
    }
}
