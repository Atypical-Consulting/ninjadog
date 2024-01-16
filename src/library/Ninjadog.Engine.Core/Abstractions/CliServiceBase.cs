// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Diagnostics;
using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Provides a base implementation for executing system commands.
/// This abstract class can be inherited by services that need to execute system or shell commands.
/// </summary>
public abstract class CliServiceBase
{
    /// <summary>
    /// Executes a system command and returns the output.
    /// </summary>
    /// <param name="command">The system command to execute, along with any arguments.</param>
    /// <returns>The output of the executed command as a string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the command execution fails.</exception>
    protected static CliCommandResult ExecuteCommand(string command)
    {
        // split the command and arguments
        var commandParts = command.Split(' ', 2);
        var commandName = commandParts[0];
        var commandArgs = commandParts.Length > 1 ? commandParts[1] : string.Empty;

        var startInfo = CreateProcessStartInfo(commandName, commandArgs);

        using var process = StartOrThrow(command, startInfo);
        using var reader = process.StandardOutput;
        var output = reader.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        var isSuccess = process.ExitCode == 0;
        var errorMessage = isSuccess ? string.Empty : error;

        return new CliCommandResult(output, isSuccess, errorMessage);
    }

    private static ProcessStartInfo CreateProcessStartInfo(string commandName, string commandArgs)
    {
        return new ProcessStartInfo
        {
            FileName = commandName,
            Arguments = commandArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
    }

    private static Process StartOrThrow(string command, ProcessStartInfo startInfo)
    {
        return Process.Start(startInfo)
               ?? throw new InvalidOperationException($"Failed to execute 'dotnet {command}'.");
    }
}
