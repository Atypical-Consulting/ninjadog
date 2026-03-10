// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.CLI.Commands;
using Ninjadog.CLI.Utilities;
using Ninjadog.Engine;
using Ninjadog.Engine.Core.DomainEvents;
using Ninjadog.Engine.Infrastructure;
using Ninjadog.Templates.CrudWebAPI.Setup;
using Ninjadog.Templates.CrudWebAPI.UseCases.RestaurantBooking;
using Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

SpectreWriteHelpers.WriteNinjadog();

var registrations = new ServiceCollection();
registrations.AddDomainEventDispatcher();
registrations.AddInfrastructure();
registrations.AddSingleton<INinjadogEngineFactory, NinjadogEngineFactory>();
registrations.AddSingleton<NinjadogTemplateManifest, CrudTemplateManifest>();
registrations.AddSingleton<NinjadogSettings, TodoAppSettings>();
var registrar = new Ninjadog.CLI.Infrastructure.TypeRegistrar(registrations);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.PropagateExceptions();

#if DEBUG
    config.ValidateExamples();
#endif

    config.AddCommand<InitCommand>("init")
        .WithDescription("Initializes a new Ninjadog project.")
        .WithExample(["init"]);

    config.AddCommand<BuildCommand>("build")
        .WithDescription("Builds and compiles the project.")
        .WithExample(["build"]);

    config.AddCommand<AddCommand>("add")
        .WithDescription("Adds a new template or module.");

    config.AddCommand<UpdateCommand>("update")
        .WithDescription("Updates templates and project files.")
        .WithExample(["update"]);

    config.AddCommand<ValidateCommand>("validate")
        .WithDescription("Validates project configuration.")
        .WithExample(["validate"]);

    config.AddCommand<DeployCommand>("deploy")
        .WithDescription("Deploys project to an environment.");

    config.AddCommand<ListTemplatesCommand>("list-templates")
        .WithDescription("Lists available templates.")
        .WithExample(["list-templates"]);

    config.AddCommand<InfoCommand>("info")
        .WithDescription("Displays project information.")
        .WithExample(["info"]);

    config.AddCommand<CleanCommand>("clean")
        .WithDescription("Cleans up the project directory.")
        .WithExample(["clean"]);

    config.AddCommand<AddIntegrationCommand>("add-integration")
        .WithDescription("Adds a new integration to the project.");

    config.AddCommand<TestCommand>("test")
        .WithDescription("Runs project tests.")
        .WithExample(["test"]);

    config.AddCommand<NinjadogCommand>("ninjadog")
        .WithDescription("Generates a new Ninjadog project.")
        .WithExample(["ninjadog"]);
});

try
{
    return app.Run(args);
}
catch (Exception ex)
{
    WriteException(ex, ExceptionFormats.ShortenEverything);
    return -99;
}
