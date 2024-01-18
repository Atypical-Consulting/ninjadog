// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Infrastructure.Services;

/// <summary>
/// A service for creating a Ninjadog app.
/// </summary>
public class NinjadogAppService : INinjadogAppService
{
    private readonly NinjadogSettings? _settings;
    private readonly NinjadogTemplateManifest? _manifest;
    private readonly ICliDotnetService _dotnet;
    private readonly IFileService _fileService;

    /// <inheritdoc />
    public string AppName { get; }

    /// <inheritdoc />
    public string AppDirectory { get; }

    /// <inheritdoc />
    public string ProjectPath { get; }

    /// <summary>
    /// Creates a new instance of <see cref="NinjadogAppService"/>.
    /// </summary>
    /// <param name="settings">The Ninjadog settings.</param>
    /// <param name="manifest">The Ninjadog template manifest.</param>
    /// <param name="dotnet">The .NET CLI service.</param>
    /// <param name="fileService">The file service.</param>
    public NinjadogAppService(
        NinjadogSettings settings,
        NinjadogTemplateManifest manifest,
        ICliDotnetService dotnet,
        IFileService fileService)
    {
        _settings = settings;
        _manifest = manifest;
        _dotnet = dotnet;
        _fileService = fileService;

        AppName = settings.Config.Name;
        AppDirectory ??= fileService.CreateAppFolder(AppName);
        ProjectPath = Path.Combine(AppDirectory, "src", $"{AppName}.{_manifest.Name}");
    }

    /// <inheritdoc />
    public virtual INinjadogAppService CreateApp(bool deleteIfExists = true)
    {
        if (deleteIfExists)
        {
            _fileService.DeleteAppFolder(_settings!.Config.Name);
        }

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
        var jsonString = _settings!.ToJsonString();
        var filePath = Path.Combine(AppDirectory, NinjadogSettingsFile);
        _fileService.CreateFile(filePath, jsonString);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewGitIgnoreFile()
    {
        _dotnet.New("gitignore", AppDirectory);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewEditorConfigFile()
    {
        _dotnet.New("editorconfig", AppDirectory);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewGlobalJsonFile()
    {
        _dotnet.New("globaljson", AppDirectory);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewSolutionFile()
    {
        _dotnet.NewSolution(AppDirectory);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService NewProjectFile()
    {
        _dotnet.New("webapiaot", ProjectPath);
        return this;
    }

    /// <inheritdoc />
    public virtual INinjadogAppService AddFileToProject(NinjadogContentFile contentFile)
    {
        var path = contentFile.Category is not null
            ? Path.Combine(ProjectPath, contentFile.Category, contentFile.FileName)
            : Path.Combine(ProjectPath, contentFile.FileName);

        _fileService.CreateFile(path, contentFile.Content);
        return this;
    }
}
