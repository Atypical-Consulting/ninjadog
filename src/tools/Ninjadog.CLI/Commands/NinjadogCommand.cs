// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.ComponentModel;
using Ninjadog.Engine;
using Ninjadog.Engine.Collections;
using Ninjadog.Engine.Configuration;
using Ninjadog.Templates.CrudWebAPI.Setup;
using Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ninjadog.CLI.Commands;

internal sealed class NinjadogCommand : Command<NinjadogCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandOption("-i|--in-memory")]
        [DefaultValue(true)]
        public bool InMemory { get; init; }

        [CommandOption("-d|--disk")]
        [DefaultValue(true)]
        public bool Disk { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        CrudTemplateManifest templateManifest = new();
        TodoAppSettings todoAppSettings = new();

        OutputProcessorCollection outputProcessors = new(
            settings.InMemory, settings.Disk);

        NinjadogEngineConfiguration configuration = new(
            templateManifest, todoAppSettings, outputProcessors);

        try
        {
            NinjadogEngineFactory
                .CreateNinjadogEngine(configuration)
                .Run();
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.WriteException(e);
            return 1;
        }

        return 0;
    }
}
