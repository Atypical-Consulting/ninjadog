// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// A service for handling file and directory operations.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Creates a subdirectory within the NinjadogProject directory.
    /// </summary>
    /// <param name="appName">The name of the application, used as the subdirectory name.</param>
    /// <returns>The path to the newly created directory.</returns>
    string CreateAppFolder(string appName);

    /// <summary>
    /// Deletes an application's directory.
    /// </summary>
    /// <param name="appName">The name of the application.</param>
    /// <returns>The path to the deleted directory.</returns>
    string DeleteAppFolder(string appName);

    /// <summary>
    /// Creates a subdirectory within an application's directory.
    /// </summary>
    /// <param name="appName">The name of the application.</param>
    /// <param name="subFolderName">The name of the subfolder to create.</param>
    /// <returns>The path to the newly created directory.</returns>
    string CreateSubFolder(string appName, string subFolderName);

    /// <summary>
    /// Creates a file with content in a specified directory.
    /// </summary>
    /// <param name="path">The path to the directory where the file will be created.</param>
    /// <param name="content">The content to write to the file.</param>
    /// <returns>The path to the newly created file.</returns>
    string CreateFile(string path, string content);
}
