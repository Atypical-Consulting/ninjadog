// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;
using Ninjadog.Engine.Abstractions;

namespace Ninjadog.Engine.Services;

public sealed class DotnetCommandService : IDotnetCommandService
{
    public DotnetCommandService()
    {
        EnsureDotnetIsAvailable();
    }

    private void EnsureDotnetIsAvailable()
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException("dotnet CLI is not available.");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to execute 'dotnet --version'.", ex);
        }
    }

    public void ExecuteCommand(string command, string args)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"{command} {args}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(startInfo);
        using (var reader = process.StandardOutput)
        {
            var result = reader.ReadToEnd();
            Console.WriteLine(result);
        }

        process.WaitForExit();
    }
}

