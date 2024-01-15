// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Collections;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Abstractions;

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
    INinjadogEngineBuilder WithOutputProcessors(OutputProcessorCollection outputProcessors);

    /// <summary>
    /// Specifies the dotnet command service to use with the Ninjadog Engine.
    /// </summary>
    /// <param name="dotnetCommandService">The dotnet command service to be used by the engine.</param>
    /// <returns>The builder instance for fluent chaining.</returns>
    INinjadogEngineBuilder WithDotnetCommandService(IDotnetCommandService dotnetCommandService);

    /// <summary>
    /// Builds and returns a configured instance of the Ninjadog Engine.
    /// </summary>
    /// <returns>A fully configured instance of the Ninjadog Engine.</returns>
    INinjadogEngine Build();

    /// <inheritdoc />
    INinjadogEngineBuilder WithFileService(IFileService fileService);
}
