// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Settings;
using Ninjadog.Templates.Engine.OutputProcessor;

namespace Ninjadog.Templates.Engine;

public class NinjadogTemplateEngine(
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings,
    List<IOutputProcessor> outputProcessors)
    : ITemplateEngine
{
    public List<string> GeneratedContents { get; } = [];

    public void Run()
    {
        GeneratedContents.Clear();

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
