// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Settings;

namespace Ninjadog.Templates;

public class NinjadogTemplateEngine(
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings)
    : ITemplateEngine
{
    public Dictionary<string, string> GeneratedFiles { get; } = [];

    public void Run()
    {
        GeneratedFiles.Clear();

        // For each template file in the manifest...
        foreach (var (path, template) in templateManifest.TemplateFiles)
        {
            // First, add a single file based on the template and the settings...
            AddFile(path, template.GenerateSingleFile(ninjadogSettings));

            // ...then, add multiple files based on the template and the settings
            foreach (var content in template.GenerateMultipleFiles(ninjadogSettings))
            {
                AddFile(path, content);
            }
        }
    }

    private void AddFile(string path, string? content)
    {
        if (content != null)
        {
            // TODO: where should we write the file?
            // write on disk or in memory?
            GeneratedFiles.Add(path, content);
        }
    }
}
