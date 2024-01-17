// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Defines the interface for a service capable of executing dotnet CLI commands.
/// This interface abstracts the functionality required to run various dotnet commands programmatically.
/// </summary>
public interface ICliDotnetService
{
    /// <summary>
    /// Gets the dotnet version.
    /// </summary>
    /// <returns>The dotnet version.</returns>
    CliCommandResult Version();

    /// <summary>
    /// Creates a new solution at the specified path.
    /// </summary>
    /// <param name="solutionPath">The path to the solution to be created.</param>
    /// <returns>The result of the solution creation.</returns>
    CliCommandResult CreateSolution(string solutionPath);

    /// <summary>
    /// Builds a project at the specified path.
    /// </summary>
    /// <param name="projectPath">The path to the project to be built.</param>
    /// <returns>The result of the build.</returns>
    CliCommandResult Build(string projectPath);

    /// <summary>
    /// Adds a NuGet package to a project.
    /// </summary>
    /// <param name="appDirectory">The path to the project to which the package will be added.</param>
    /// <param name="package">The name of the package to be added.</param>
    /// <returns>The result of the command execution.</returns>
    CliCommandResult AddPackage(string appDirectory, string package);

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="templateKey">The template of the project to create (e.g., 'console', 'webapi').</param>
    /// <param name="outputPath">The output directory for the created project.</param>
    /// <returns>The result of the command execution.</returns>
    CliCommandResult CreateProject(string templateKey, string outputPath);

    /// <summary>
    /// Publishes a project with Ahead-of-Time (AOT) compilation.
    /// </summary>
    /// <param name="projectPath">The path to the project to publish.</param>
    /// <param name="runtimeIdentifier">The runtime identifier (e.g., win-x64, linux-x64).</param>
    /// <returns>The result of the command execution.</returns>
    CliCommandResult PublishWithAOT(string projectPath, string runtimeIdentifier);
}
