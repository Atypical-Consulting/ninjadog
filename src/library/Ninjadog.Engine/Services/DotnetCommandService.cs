// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;
using Ninjadog.Engine.Abstractions;

namespace Ninjadog.Engine.Services;

/// <summary>
/// A service for executing .NET CLI commands.
/// This class provides functionality to programmatically run commands using the .NET CLI,
/// ensuring the CLI is available upon initialization.
/// </summary>
public sealed class DotnetCommandService : IDotnetCommandService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DotnetCommandService"/>.
    /// During initialization, it checks the availability of the .NET CLI on the system.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the .NET CLI is not available or fails to execute.</exception>
    public DotnetCommandService()
    {
        if (string.IsNullOrEmpty(Version()))
        {
            throw new InvalidOperationException("The .NET CLI is not available.");
        }
    }

    /// <inheritdoc />
    public string ExecuteCommand(string command, string? args = null)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"{command} {args}".Trim(),
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process =
            Process.Start(startInfo)
            ?? throw new InvalidOperationException($"Failed to execute 'dotnet {command} {args}'.");

        using var reader = process.StandardOutput;
        var result = reader.ReadToEnd();

        process.WaitForExit();

        return result;
    }

    /// <inheritdoc />
    public string Build(string projectPath)
    {
        return ExecuteCommand("build", projectPath);
    }

    /// <inheritdoc />
    public string? Version()
    {
        return ExecuteCommand("--version");
    }
}

