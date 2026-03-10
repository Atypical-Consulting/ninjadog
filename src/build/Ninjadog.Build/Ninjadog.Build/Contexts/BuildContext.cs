// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Build.Contexts;

/// <summary>
/// The build context.
/// </summary>
public class BuildContext(ICakeContext context) : FrostingContext(context)
{
    /// <summary>
    /// Gets or sets the MSBuild configuration.
    /// </summary>
    public string MsBuildConfiguration { get; set; } = context.Argument("configuration", "Release");
}
