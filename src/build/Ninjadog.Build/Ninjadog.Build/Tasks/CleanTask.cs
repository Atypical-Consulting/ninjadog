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

        const string basePath = "../../src";
        var directories = new List<string>
        {
            $"library/Ninjadog.Engine/bin/{context.MsBuildConfiguration}",
            $"library/Ninjadog.Engine.Core/bin/{context.MsBuildConfiguration}",
            $"library/Ninjadog.Engine.Infrastructure/bin/{context.MsBuildConfiguration}",
            $"library/Ninjadog.Helpers/bin/{context.MsBuildConfiguration}",
            $"library/Ninjadog.Settings/bin/{context.MsBuildConfiguration}",
            $"library/Ninjadog.Settings.Extensions/bin/{context.MsBuildConfiguration}",
            $"saas/Ninjadog.SaaS/bin/{context.MsBuildConfiguration}",
            $"saas/Ninjadog.SaaS.ServiceDefaults/bin/{context.MsBuildConfiguration}",
            $"saas/Ninjadog.SaaS.WebApp/bin/{context.MsBuildConfiguration}",
            $"templates/Ninjadog.Templates.CrudWebApi/bin/{context.MsBuildConfiguration}",
            $"tools/Ninjadog.CLI/bin/{context.MsBuildConfiguration}"
        };

        foreach (var dir in directories)
        {
            var fullPath = $"{basePath}/{dir}";
            context.CleanDirectory(fullPath);
            context.Log.Information($"Cleaned {fullPath}");
        }
    }
}
