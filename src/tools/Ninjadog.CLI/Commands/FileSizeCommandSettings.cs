// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace Ninjadog.CLI.Commands;

/// <summary>
/// Represents the settings for the file size command, specifying the configuration options
/// such as search path, search pattern, and whether to include hidden files.
/// </summary>
public sealed class FileSizeCommandSettings : CommandSettings
{
    /// <summary>
    /// Gets or initializes the path where the file size search will be conducted.
    /// Defaults to the current directory if not specified.
    /// </summary>
    [Description("Path to search. Defaults to current directory.")]
    [CommandArgument(0, "[searchPath]")]
    public string? SearchPath { get; init; }

    /// <summary>
    /// Gets or initializes the search pattern to filter files for size calculation.
    /// For example, "*.txt" will include only text files.
    /// </summary>
    [CommandOption("-p|--pattern")]
    public string? SearchPattern { get; init; }

    /// <summary>
    /// Gets a value indicating whether to include hidden files in the search.
    /// </summary>
    [CommandOption("--hidden")]
    [DefaultValue(true)]
    public bool IncludeHidden { get; init; }
}
