// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Defines the interface for the Ninjadog application service,
/// responsible for initializing and setting up various components of a Ninjadog application.
/// </summary>
public interface INinjadogAppService
{
    /// <summary>
    /// Initializes the service with the provided settings and template manifest.
    /// </summary>
    /// <param name="settings">The settings to be used by the Ninjadog application.</param>
    /// <param name="manifest">The template manifest to be used by the Ninjadog application.</param>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService Initialize(NinjadogSettings settings, NinjadogTemplateManifest manifest);

    /// <summary>
    /// Creates the application based on the initialized settings and manifest.
    /// </summary>
    /// <param name="deleteIfExists">Whether to delete the application folder if it already exists.</param>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService CreateApp(bool deleteIfExists = true);

    /// <summary>
    /// Creates a Ninjadog settings file for the application.
    /// </summary>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService NewNinjadogSettingsFile();

    /// <summary>
    /// Creates a .gitignore file suitable for the application.
    /// </summary>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService NewGitIgnoreFile();

    /// <summary>
    /// Creates an EditorConfig file to maintain consistent coding styles for various editors and IDEs.
    /// </summary>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService NewEditorConfigFile();

    /// <summary>
    /// Creates a global.json file to specify the .NET SDK version to be used.
    /// </summary>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService NewGlobalJsonFile();

    /// <summary>
    /// Creates the solution file for the application.
    /// </summary>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService NewSolutionFile();

    /// <summary>
    /// Creates the project file for the application.
    /// </summary>
    /// <returns>The service instance for chaining.</returns>
    INinjadogAppService NewProjectFile();
}
