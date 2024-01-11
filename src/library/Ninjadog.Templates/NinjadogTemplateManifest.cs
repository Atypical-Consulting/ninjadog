// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Templates;

public abstract class NinjadogTemplateManifest
{
    public virtual string Name { get; init; }
    public virtual string Description { get; init; }
    public virtual string Version { get; init; }
    public virtual string Author { get; init; }
    public virtual string License { get; init; }
    public virtual NinjadogTemplateFiles TemplateFiles { get; init; }
}
