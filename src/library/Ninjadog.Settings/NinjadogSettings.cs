// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Settings;

public record NinjadogSettings
{
    public virtual NinjadogConfiguration Config { get; init; }
    public virtual NinjadogEntities Entities { get; init; }
}
