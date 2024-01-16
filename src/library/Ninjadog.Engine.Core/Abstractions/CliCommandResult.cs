// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Represents the result of a CLI command execution.
/// </summary>
/// <param name="Output">The output of the command.</param>
/// <param name="IsSuccess">A value indicating whether the command was successful.</param>
/// <param name="ErrorMessage">The error message, if any.</param>
public record CliCommandResult(
    string Output,
    bool IsSuccess,
    string ErrorMessage = "");
