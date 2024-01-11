// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Engine.Collections;
using Ninjadog.Settings;
using Ninjadog.Templates;

namespace Ninjadog.Engine.Configuration;

public record NinjadogEngineConfiguration(
    NinjadogTemplateManifest TemplateManifest,
    NinjadogSettings NinjadogSettings,
    OutputProcessorCollection OutputProcessors);
