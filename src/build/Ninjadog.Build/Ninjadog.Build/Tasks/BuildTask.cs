// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Build.Tasks;

/// <summary>
/// The build task.
/// </summary>
[TaskName("Build")]
[IsDependentOn(typeof(CleanTask))]
public sealed class BuildTask : FrostingTask<BuildContext>
{
    /// <inheritdoc/>
    public override void Run(BuildContext context)
    {
        var buildSettings = new DotNetBuildSettings
        {
            Configuration = context.MsBuildConfiguration,
        };

        context.DotNetBuild(context.NinjadogSln, buildSettings);
    }
}
