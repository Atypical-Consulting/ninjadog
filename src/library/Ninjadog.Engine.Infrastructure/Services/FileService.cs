// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;
using static System.Environment;

namespace Ninjadog.Engine.Infrastructure.Services;

/// <summary>
/// A service for handling file and directory operations.
/// </summary>
public class FileService : IFileService
{
    private const SpecialFolder UserProfile = SpecialFolder.UserProfile;
    private readonly string _baseFolder;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileService"/> class.
    /// </summary>
    public FileService()
    {
        _baseFolder = Path.Combine(GetFolderPath(UserProfile), "NinjadogProjects");
        EnsureDirectoryExists(_baseFolder);
    }

    /// <inheritdoc />
    public string CreateAppFolder(string appName)
    {
        var appFolderPath = Path.Combine(_baseFolder, appName);
        EnsureDirectoryExists(appFolderPath);
        return appFolderPath;
    }

    /// <inheritdoc />
    public void DeleteAppFolder(string appName)
    {
        var appFolderPath = Path.Combine(_baseFolder, appName);
        EnsureDirectoryExists(appFolderPath);
        Directory.Delete(appFolderPath, true);
    }

    /// <inheritdoc />
    public string CreateSubFolder(string appName, string subFolderName)
    {
        var subFolderPath = Path.Combine(_baseFolder, appName, subFolderName);
        EnsureDirectoryExists(subFolderPath);
        return subFolderPath;
    }

    /// <inheritdoc />
    public string CreateFile(string path, string content)
    {
        var filePath = Path.Combine(_baseFolder, path);
        EnsureDirectoryExists(Path.GetDirectoryName(filePath) ?? string.Empty);
        File.WriteAllText(filePath, content);
        return filePath;
    }

    /// <summary>
    /// Ensures that a directory exists, creating it if it does not.
    /// </summary>
    /// <param name="path">The path of the directory to check or create.</param>
    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
