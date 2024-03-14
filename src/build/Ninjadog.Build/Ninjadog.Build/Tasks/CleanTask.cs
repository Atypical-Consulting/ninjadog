// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Cake.Common.IO;

namespace Ninjadog.Build.Tasks;

/// <summary>
/// The clean task.
/// </summary>
[TaskName("Clean")]
public sealed class CleanTask : FrostingTask<BuildContext>
{
    /// <inheritdoc/>
    public override void Run(BuildContext context)
    {
        context.Log.Information("Cleaning the build directories...");

        context.CleanDirectory($"../../src/library/Ninjadog.Engine/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/library/Ninjadog.Engine.Core/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/library/Ninjadog.Engine.Infrastructure/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/library/Ninjadog.Helpers/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/library/Ninjadog.Settings/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/library/Ninjadog.Settings.Extensions/bin/{context.MsBuildConfiguration}");

        context.CleanDirectory($"../../src/saas/Ninjadog.SaaS/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/saas/Ninjadog.SaaS.ServiceDefaults/bin/{context.MsBuildConfiguration}");
        context.CleanDirectory($"../../src/saas/Ninjadog.SaaS.WebApp/bin/{context.MsBuildConfiguration}");

        context.CleanDirectory($"../../src/templates/Ninjadog.Templates.CrudWebApi/bin/{context.MsBuildConfiguration}");

        context.CleanDirectory($"../../src/tools/Ninjadog.CLI/bin/{context.MsBuildConfiguration}");
    }
}
