// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;
using Ninjadog.Engine.Core.Models;
using Ninjadog.Settings;

namespace Ninjadog.Engine.Infrastructure.Services;

/// <summary>
/// A service for creating a Ninjadog app.
/// </summary>
/// <param name="dotnet">The .NET CLI service.</param>
/// <param name="fileService">The file service.</param>
public class NinjadogAppService(ICliDotnetService dotnet, IFileService fileService)
    : INinjadogAppService
{
    private NinjadogSettings? _settings;
    private NinjadogTemplateManifest? _manifest;
    private string? _appDirectory;
    private bool _isInitialized;

    /// <inheritdoc />
    public virtual INinjadogAppService Initialize(
        NinjadogSettings settings,
        NinjadogTemplateManifest manifest)
    {
        _settings = settings;
        _manifest = manifest;

        var appName = settings.Config.Name;
        _appDirectory ??= fileService.CreateAppFolder(appName);
        _isInitialized = true;

        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService CreateApp()
    {
        ThrowIfNotInitialized();
        NewNinjadogSettingsFile();
        NewEditorConfigFile();
        NewGitIgnoreFile();
        NewGlobalJsonFile();
        NewSolutionFile();
        NewProjectFile();

        // TODO: Add NuGet packages from manifest and build the app
        // var dotnetVersion = cliDotnetService.Version();
        // var buildResult = cliDotnetService.Build(appDirectory);

        // Install NuGet packages
        // foreach (var package in templateManifest.NuGetPackages)
        // {
        //     cliDotnetService.AddPackage(appDirectory, package);
        // }

        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewNinjadogSettingsFile()
    {
        ThrowIfNotInitialized();
        var jsonString = _settings!.ToJsonString();
        var filePath = Path.Combine(_appDirectory!, NinjadogSettingsFile);
        fileService.CreateFile(filePath, jsonString);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewGitIgnoreFile()
    {
        ThrowIfNotInitialized();
        dotnet.New("gitignore", _appDirectory!);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewEditorConfigFile()
    {
        ThrowIfNotInitialized();
        dotnet.New("editorconfig", _appDirectory!);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewGlobalJsonFile()
    {
        ThrowIfNotInitialized();
        dotnet.New("globaljson", _appDirectory!);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewSolutionFile()
    {
        ThrowIfNotInitialized();
        dotnet.NewSolution(_appDirectory!);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewProjectFile()
    {
        ThrowIfNotInitialized();
        var outputPath = Path.Combine(_appDirectory!, _manifest!.Name);
        dotnet.New("web", outputPath);
        return this;
    }

    private void ThrowIfNotInitialized()
    {
        if (!_isInitialized)
        {
            throw new InvalidOperationException("The app is not initialized. Call Initialize() first.");
        }
    }
}
