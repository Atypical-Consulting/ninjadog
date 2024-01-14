// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for a service capable of executing dotnet CLI commands.
/// This interface abstracts the functionality required to run various dotnet commands programmatically.
/// </summary>
public interface IDotnetCommandService
{
    /// <summary>
    /// Executes a specified dotnet CLI command with the given arguments.
    /// </summary>
    /// <param name="command">The dotnet command to be executed (e.g., "build", "run").</param>
    /// <param name="args">The arguments to pass to the dotnet command.</param>
    /// <returns>The result of the dotnet command.</returns>
    string ExecuteCommand(string command, string args);

    /// <summary>
    /// Builds a project at the specified path.
    /// </summary>
    /// <param name="projectPath">The path to the project to be built.</param>
    /// <returns>The result of the build.</returns>
    string Build(string projectPath);

    /// <summary>
    /// Gets the dotnet version.
    /// </summary>
    /// <returns>The dotnet version.</returns>
    string? Version();
}
