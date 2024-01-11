// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.Configuration;
using Ninjadog.Engine.Internals;

namespace Ninjadog.Engine;

public static class NinjadogEngineFactory
{
    public static INinjadogEngine CreateNinjadogEngine(
        NinjadogEngineConfiguration configuration)
    {
        var ninjadogEngine = new NinjadogEngineBuilder()
            .WithManifest(configuration.TemplateManifest)
            .WithSettings(configuration.NinjadogSettings);

        foreach (var outputProcessor in configuration.OutputProcessors)
        {
            ninjadogEngine.AddOutputProcessor(outputProcessor);
        }

        return ninjadogEngine.Build();
    }
}
