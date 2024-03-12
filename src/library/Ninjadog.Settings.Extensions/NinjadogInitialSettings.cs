// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Represents the initial settings for the Ninjadog use case.
/// </summary>
public sealed record NinjadogInitialSettings : NinjadogSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NinjadogInitialSettings"/> class.
    /// </summary>
    /// <param name="name">The name of the Ninjadog app. Default is "NinjadogApp".</param>
    /// <param name="version">The version of the Ninjadog app. Default is "1.0.0".</param>
    /// <param name="description">The description of the Ninjadog app. Default is "Welcome to Ninjadog!".</param>
    /// <param name="rootNamespace">The root namespace of the Ninjadog app. Default is "NinjadogApp".</param>
    /// <param name="outputPath">The output path of the Ninjadog app. Defaults to "src/applications/[name]".</param>
    public NinjadogInitialSettings(
        string? name = null,
        string? version = null,
        string? description = null,
        string? rootNamespace = null,
        string? outputPath = null)
        : base(
            new NinjadogInitialConfiguration(name, version, description, rootNamespace, outputPath),
            new NinjadogInitialEntities())
    {
    }
}
