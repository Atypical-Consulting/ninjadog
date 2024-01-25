// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Ninjadog.Settings.Config;

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Provides the specific configuration for the "Ninjadog" application.
/// This sealed record inherits from NinjadogConfiguration and sets predefined values
/// tailored for the Ninjadog project, such as its name, version, description, and paths.
/// </summary>
public sealed record NinjadogInitialConfiguration()
    : NinjadogConfiguration(
        Name: "NinjadogApp",
        Version: "1.0.0",
        Description: "Welcome to Ninjadog!",
        RootNamespace: "NinjadogApp",
        OutputPath: "src/applications/NinjadogApp");
