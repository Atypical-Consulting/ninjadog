// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.Collections;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Internals;

internal sealed class NinjadogEngine(
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings,
    OutputProcessorCollection outputProcessors)
    : INinjadogEngine
{
    public event EventHandler<NinjadogContentFile>? FileGenerated;

    public void Run()
    {
        foreach (var template in templateManifest.Templates)
        {
            Run(template);
        }
    }

    private void Run(NinjadogTemplate template)
    {
        // First, add a single file based on the template and the settings...
        var singleFileContent = template.GenerateOne(ninjadogSettings);
        ProcessContent(singleFileContent);

        // ...then, add multiple files based on the template and the settings
        foreach (var content in template.GenerateMany(ninjadogSettings))
        {
            ProcessContent(content);
        }
    }

    private void ProcessContent(NinjadogContentFile? contentFile)
    {
        if (contentFile is null || string.IsNullOrEmpty(contentFile.Content))
        {
            return;
        }

        foreach (var processor in outputProcessors)
        {
            processor.ProcessOutput(contentFile);
        }

        OnFileGenerated(contentFile);
    }

    private void OnFileGenerated(NinjadogContentFile contentFile)
    {
        FileGenerated?.Invoke(this, contentFile);
    }
}
