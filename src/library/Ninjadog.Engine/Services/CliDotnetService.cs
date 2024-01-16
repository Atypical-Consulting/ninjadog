// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Services;

/// <summary>
/// A service for executing .NET CLI commands.
/// This class provides functionality to programmatically run commands using the .NET CLI,
/// ensuring the CLI is available upon initialization.
/// </summary>
public sealed class CliDotnetService
    : CliServiceBase, IDotnetCliService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CliDotnetService"/>.
    /// During initialization, it checks the availability of the .NET CLI on the system.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the .NET CLI is not available or fails to execute.</exception>
    public CliDotnetService()
    {
        if (Version().IsSuccess == false)
        {
            throw new InvalidOperationException("The .NET CLI is not available.");
        }
    }

    /// <inheritdoc />
    public CliCommandResult Version()
    {
        return ExecuteCommand("dotnet --version");
    }

    /// <inheritdoc />
    public CliCommandResult CreateSolution(string solutionPath)
    {
        return ExecuteCommand($"dotnet new sln --output {solutionPath}");
    }

    /// <inheritdoc />
    public CliCommandResult Build(string projectPath)
    {
        return ExecuteCommand($"dotnet build {projectPath}");
    }

    /// <inheritdoc />
    public CliCommandResult AddPackage(string projectPath, string package)
    {
        return ExecuteCommand($"dotnet add {projectPath} package {package}");
    }

    /// <inheritdoc />
    public CliCommandResult CreateProject(string template, string outputPath)
    {
        return ExecuteCommand($"dotnet new {template} --output {outputPath}");
    }

    /// <inheritdoc />
    public CliCommandResult PublishWithAOT(string projectPath, string runtimeIdentifier)
    {
        return ExecuteCommand(
            $"dotnet publish {projectPath} -c Release -r {runtimeIdentifier} " +
            $"--self-contained -p:PublishReadyToRun=true");
    }
}
