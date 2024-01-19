// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Represents the result of a CLI command execution.
/// </summary>
public record CliCommandResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CliCommandResult"/> class.
    /// </summary>
    /// <param name="output">The output of the command.</param>
    /// <param name="isSuccess">A value indicating whether the command was successful.</param>
    /// <param name="errorMessage">The error message, if any.</param>
    public CliCommandResult(
        string output,
        bool isSuccess,
        string errorMessage = "")
    {
        this.Output = output.Trim();
        this.IsSuccess = isSuccess;
        this.ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Gets the output of the command.
    /// </summary>
    public string Output { get; init; }

    /// <summary>
    /// Gets a value indicating whether a value indicating whether the command was successful.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the error message, if any.
    /// </summary>
    public string ErrorMessage { get; init; }
}
