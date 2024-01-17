// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.OutputProcessors;

namespace Ninjadog.Engine.OutputProcessors;

/// <summary>
/// An output processor that writes generated content to the filesystem.
/// This class implements the <see cref="IOutputProcessor"/> interface and provides functionality to write
/// output content to disk, facilitating persistence and file-based operations.
/// </summary>
/// <param name="fileService">The file service to use for writing to the filesystem.</param>
public class DiskOutputProcessor(IFileService fileService)
    : IDiskOutputProcessor
{
    /// <summary>
    /// Processes the given content by writing it to the filesystem.
    /// </summary>
    /// <param name="templateManifest">The template manifest to be used by the engine.</param>
    /// <param name="ninjadogSettings">The ninjadog app settings to configure the engine.</param>
    /// <param name="contentFile">The content file to be generated and written to disk.</param>
    public void ProcessOutput(
        NinjadogTemplateManifest templateManifest,
        NinjadogSettings ninjadogSettings,
        NinjadogContentFile contentFile)
    {
        var appName = ninjadogSettings.Config.Name;
        var templateName = templateManifest.Name;
        var path = Path.Combine(appName, templateName, contentFile.OutputPath);
        fileService.CreateFile(path, contentFile.Content);
    }
}
