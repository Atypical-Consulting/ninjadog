// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.ValueObjects;

namespace Ninjadog.Engine.Infrastructure.OutputProcessors;

/// <summary>
/// An output processor that writes generated content to the filesystem.
/// This class implements the <see cref="IOutputProcessor"/> interface and provides functionality to write
/// output content to disk, facilitating persistence and file-based operations.
/// </summary>
public class DiskOutputProcessor(INinjadogAppService ninjadogAppService)
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
        ninjadogAppService.AddFileToProject(contentFile);
    }
}
