// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.OutputProcessor;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine;

internal class NinjadogEngine(
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings,
    List<IOutputProcessor> outputProcessors)
    : INinjadogEngine
{
    public void Run()
    {
        // For each template file in the manifest...
        foreach (var template in templateManifest.TemplateFiles)
        {
            // First, add a single file based on the template and the settings...
            var singleFileContent = template.GenerateSingleFile(ninjadogSettings);
            ProcessContent(singleFileContent);

            // ...then, add multiple files based on the template and the settings
            foreach (var content in template.GenerateMultipleFiles(ninjadogSettings))
            {
                ProcessContent(content);
            }
        }
    }
    private void ProcessContent(string? content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        foreach (var processor in outputProcessors)
        {
            processor.ProcessOutput(content);
        }
    }
}
