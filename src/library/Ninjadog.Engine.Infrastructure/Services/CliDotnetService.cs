// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Infrastructure.Services;

/// <summary>
/// A service for executing .NET CLI commands.
/// This class provides functionality to programmatically run commands using the .NET CLI,
/// ensuring the CLI is available upon initialization.
/// </summary>
public sealed class CliDotnetService
    : CliServiceBase, ICliDotnetService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CliDotnetService"/> class.
    /// During initialization, it checks the availability of the .NET CLI on the system.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the .NET CLI is not available or fails to execute.</exception>
    public CliDotnetService()
    {
        if (!Version().IsSuccess)
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
    public CliCommandResult NewSolution(string solutionPath)
    {
        return New("sln", solutionPath);
    }

    /// <inheritdoc />
    public CliCommandResult New(string templateKey, string outputPath)
    {
        return ExecuteCommand($"dotnet new {templateKey} --output {outputPath}");
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
    public CliCommandResult PublishWithAOT(string projectPath, string runtimeIdentifier)
    {
        return ExecuteCommand(
            $"dotnet publish {projectPath} -c Release -r {runtimeIdentifier} " +
            $"--self-contained -p:PublishReadyToRun=true");
    }
}
