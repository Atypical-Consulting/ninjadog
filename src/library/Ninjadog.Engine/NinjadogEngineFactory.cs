// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
