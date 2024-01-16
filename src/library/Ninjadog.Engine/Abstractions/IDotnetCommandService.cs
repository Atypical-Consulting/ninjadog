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
    /// Gets the dotnet version.
    /// </summary>
    /// <returns>The dotnet version.</returns>
    string? Version();

    /// <summary>
    /// Creates a new solution at the specified path.
    /// </summary>
    /// <param name="solutionPath">The path to the solution to be created.</param>
    /// <returns>The result of the solution creation.</returns>
    string CreateSolution(string solutionPath);

    /// <summary>
    /// Builds a project at the specified path.
    /// </summary>
    /// <param name="projectPath">The path to the project to be built.</param>
    /// <returns>The result of the build.</returns>
    string Build(string projectPath);
}
