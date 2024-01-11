// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Settings;

namespace Ninjadog.Core.SettingsExtensions;

public record NinjadogEntityId()
    : NinjadogEntityProperty(nameof(Guid), true);
