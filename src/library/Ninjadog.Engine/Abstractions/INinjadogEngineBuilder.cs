// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Abstractions;

public interface INinjadogEngineBuilder
{
    INinjadogEngineBuilder WithManifest(NinjadogTemplateManifest templateManifest);
    INinjadogEngineBuilder WithSettings(NinjadogSettings ninjadogSettings);
    INinjadogEngineBuilder AddOutputProcessor(IOutputProcessor outputProcessor);
    INinjadogEngine Build();
}
