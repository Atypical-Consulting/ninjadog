// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

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
    public override string Version { get; init; } = "1.0.0-alpha.2";

    /// <inheritdoc />
    public override string Author { get; init; } = "Philippe Matray";

    /// <inheritdoc />
    public override string License { get; init; } = "All rights reserved";

    /// <inheritdoc />
    public override List<string> NuGetPackages { get; init; } =
    [
        "Dapper",
        "FastEndpoints",
        "FastEndpoints.ClientGen",
        "FastEndpoints.Swagger",
        "FluentValidation",
        "ValueOf",
        "Microsoft.Data.Sqlite"
    ];

    /// <inheritdoc />
    public override NinjadogTemplates Templates { get; init; } = new CrudTemplates();
}
