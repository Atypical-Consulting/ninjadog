// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Abstractions;

/// <summary>
/// Defines the interface for a builder responsible for constructing and configuring instances of the Ninjadog Engine.
/// This interface allows for a fluent, step-by-step configuration of the engine before its creation.
/// </summary>
public interface INinjadogEngineBuilder
{
    /// <summary>
    /// Specifies the template manifest to use with the Ninjadog Engine.
    /// </summary>
    /// <param name="templateManifest">The template manifest to be used by the engine.</param>
    /// <returns>The builder instance for fluent chaining.</returns>
    INinjadogEngineBuilder WithManifest(NinjadogTemplateManifest templateManifest);

    /// <summary>
    /// Specifies the settings to use with the Ninjadog Engine.
    /// </summary>
    /// <param name="ninjadogSettings">The settings to configure the engine.</param>
    /// <returns>The builder instance for fluent chaining.</returns>
    INinjadogEngineBuilder WithSettings(NinjadogSettings ninjadogSettings);

    /// <summary>
    /// Specifies the output processor to use with the Ninjadog Engine.
    /// </summary>
    /// <param name="outputProcessor">The output processor to be used by the engine.</param>
    /// <returns>The builder instance for fluent chaining.</returns>
    INinjadogEngineBuilder WithOutputProcessor(IOutputProcessor outputProcessor);

    /// <summary>
    /// Specifies the output processors to use with the Ninjadog Engine.
    /// </summary>
    /// <param name="outputProcessors">The output processors to be used by the engine.</param>
    /// <returns>The builder instance for fluent chaining.</returns>
    INinjadogEngineBuilder WithOutputProcessors(List<IOutputProcessor> outputProcessors);

    /// <summary>
    /// Specifies the service provider to use with the Ninjadog Engine.
    /// </summary>
    /// <param name="serviceProvider">The service provider to be used by the engine.</param>
    INinjadogEngineBuilder WithServiceProvider(IServiceProvider serviceProvider);

    /// <summary>
    /// Builds and returns a configured instance of the Ninjadog Engine.
    /// </summary>
    /// <returns>A fully configured instance of the Ninjadog Engine.</returns>
    INinjadogEngine Build();
}
