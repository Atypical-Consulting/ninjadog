// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.CLI.Commands;
using Ninjadog.CLI.Utilities;
using Ninjadog.Settings;
using Ninjadog.Templates;
using Ninjadog.Templates.CrudWebAPI.Setup;
using Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;
using Spectre.Console.Cli;

SpectreWriteHelpers.WriteNinjadog();

var registrations = new ServiceCollection();
registrations.AddSingleton<NinjadogTemplateManifest, CrudTemplateManifest>();
registrations.AddSingleton<NinjadogSettings, TodoAppSettings>();
var registrar = new Ninjadog.CLI.Infrastructure.TypeRegistrar(registrations);

var app = new CommandApp(registrar);

app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif

    config.AddCommand<NinjadogCommand>("ninjadog")
        .WithDescription("Generates a new Ninjadog project.")
        .WithExample(["ninjadog"]);
});

return app.Run(args);
