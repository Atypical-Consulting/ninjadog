// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Build.Contexts;

/// <summary>
/// The build context.
/// </summary>
public class BuildContext : FrostingContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildContext"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public BuildContext(ICakeContext context)
        : base(context)
    {
        MsBuildConfiguration = context.Argument("configuration", "Release");
        NinjadogSln = "../../Ninjadog.sln";
    }

    /// <summary>
    /// Gets or sets the MSBuild configuration.
    /// </summary>
    public string MsBuildConfiguration { get; set; }

    /// <summary>
    /// Gets or sets the Ninjadog solution path.
    /// </summary>
    public string NinjadogSln { get; set; }
}
