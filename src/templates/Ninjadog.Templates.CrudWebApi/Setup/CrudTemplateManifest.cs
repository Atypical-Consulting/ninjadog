// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
