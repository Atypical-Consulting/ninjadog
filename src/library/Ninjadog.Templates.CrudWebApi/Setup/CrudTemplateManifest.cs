// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Templates.CrudWebAPI.Setup;

public class CrudTemplateManifest : NinjadogTemplateManifest
{
    public override string Name { get; init; } = "CrudWebAPI";
    public override string Description { get; init; } = "Your ASP.NET Core Web API project with CRUD operations";
    public override string Version { get; init; } = "1.0.0-alpha.1";
    public override string Author { get; init; } = "Philippe Matray";
    public override string License { get; init; } = "All rights reserved";
    public override NinjadogTemplateFiles TemplateFiles { get; init; } = new CrudTemplateFiles();
}
