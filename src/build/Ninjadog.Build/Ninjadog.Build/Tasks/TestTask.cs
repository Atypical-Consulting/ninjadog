// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Cake.Common.Tools.DotNet.Test;

namespace Ninjadog.Build.Tasks;

/// <summary>
/// The test task.
/// </summary>
[TaskName("Test")]
[IsDependentOn(typeof(BuildTask))]
public sealed class TestTask : FrostingTask<BuildContext>
{
    /// <inheritdoc/>
    public override void Run(BuildContext context)
    {
        var dotNetTestSettings = new DotNetTestSettings
        {
            Configuration = context.MsBuildConfiguration,
            NoBuild = true,
        };

        context.DotNetTest(context.NinjadogSln, dotNetTestSettings);
    }
}
