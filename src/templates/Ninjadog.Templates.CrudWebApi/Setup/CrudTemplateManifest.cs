// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Templates.CrudWebAPI.Setup;

/// <summary>
/// Represents the manifest for the CrudWebAPI template.
/// </summary>
public class CrudTemplateManifest : NinjadogTemplateManifest
{
    /// <inheritdoc />
    public override string Name { get; init; } = "CrudWebAPI";

    /// <inheritdoc />
    public override string Description { get; init; } = "Your ASP.NET Core Web API project with CRUD operations";

    /// <inheritdoc />
    public override string Version { get; init; } = "1.0.0-alpha.1";

    /// <inheritdoc />
    public override string Author { get; init; } = "Philippe Matray";

    /// <inheritdoc />
    public override string License { get; init; } = "All rights reserved";

    /// <inheritdoc />
    public override NinjadogTemplates Templates { get; init; } = new CrudTemplates();
}
