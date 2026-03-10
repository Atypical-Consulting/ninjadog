// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace Ninjadog.CLI.Commands;

internal sealed class NinjadogCommandSettings : CommandSettings
{
    [CommandOption("-i|--in-memory")]
    [DefaultValue(true)]
    public bool InMemory { get; init; }

    [CommandOption("-d|--disk")]
    [DefaultValue(true)]
    public bool Disk { get; init; }
}
