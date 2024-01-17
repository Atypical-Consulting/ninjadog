// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine;

/// <summary>
/// Provides functionality to create and configure instances of the Ninjadog Engine.
/// This factory class abstracts the construction process of the Ninjadog Engine,
/// allowing for flexible and streamlined engine setup based on given configurations.
/// </summary>
public class NinjadogEngineFactory(IServiceProvider serviceProvider)
    : INinjadogEngineFactory
{
    /// <summary>
    /// Creates and configures a new instance of the Ninjadog Engine using the specified configuration.
    /// This method initializes a Ninjadog Engine with the provided template manifest, settings,
    /// and output processors. It facilitates the creation of a tailored engine instance
    /// suitable for specific templating tasks.
    /// </summary>
    /// <param name="configuration">The configuration settings used to set up the Ninjadog Engine.</param>
    /// <returns>A configured instance of the Ninjadog Engine.</returns>
    public INinjadogEngine CreateNinjadogEngine(
        NinjadogEngineConfiguration configuration)
    {
        return new NinjadogEngineBuilder()
            .WithManifest(configuration.TemplateManifest)
            .WithSettings(configuration.NinjadogSettings)
            .WithOutputProcessors(configuration.OutputProcessors)
            .WithServiceProvider(serviceProvider)
            .Build();
    }
}
