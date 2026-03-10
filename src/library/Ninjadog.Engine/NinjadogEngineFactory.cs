// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;

namespace Ninjadog.Engine;

/// <summary>
/// Provides functionality to create and configure instances of the Ninjadog Engine.
/// This factory class abstracts the construction process of the Ninjadog Engine,
/// allowing for flexible and streamlined engine setup based on given configurations.
/// </summary>
public class NinjadogEngineFactory(IServiceProvider serviceProvider)
    : INinjadogEngineFactory
{
    /// <inheritdoc/>
    public INinjadogEngine CreateNinjadogEngine()
    {
        var templateManifest = serviceProvider.GetRequiredService<NinjadogTemplateManifest>();
        var ninjadogSettings = serviceProvider.GetRequiredService<NinjadogSettings>();

        NinjadogOutputProcessors outputProcessors = new(serviceProvider);
        NinjadogEngineConfiguration configuration = new(templateManifest, ninjadogSettings, outputProcessors);

        return new NinjadogEngineBuilder()
            .WithManifest(configuration.TemplateManifest)
            .WithSettings(configuration.NinjadogSettings)
            .WithOutputProcessors(configuration.OutputProcessors)
            .WithServiceProvider(serviceProvider)
            .Build();
    }
}
