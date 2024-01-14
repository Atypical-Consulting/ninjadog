// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.ComponentModel;
using Ninjadog.Engine;
using Ninjadog.Engine.Collections;
using Ninjadog.Engine.Configuration;
using Ninjadog.Templates;
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
        AnsiConsole.MarkupLine("[bold]Using the following settings:[/]");
        AnsiConsole.MarkupLine($"- InMemory: [green]{settings.InMemory}[/]");
        AnsiConsole.MarkupLine($"- Disk    : [green]{settings.Disk}[/]");
        AnsiConsole.WriteLine();

        CrudTemplateManifest templateManifest = new();
        TodoAppSettings todoAppSettings = new();
        OutputProcessorCollection outputProcessors = new(settings.InMemory, settings.Disk);
        NinjadogEngineConfiguration configuration = new(templateManifest, todoAppSettings, outputProcessors);

        AnsiConsole.MarkupLine("[bold]Building the Ninjadog Engine...[/]");
        var ninjadogEngine = NinjadogEngineFactory.CreateNinjadogEngine(configuration);
        ninjadogEngine.FileGenerated += OnFileGenerated;

        try
        {
            AnsiConsole.MarkupLine("[bold]Generating files...[/]");
            ninjadogEngine.Run();
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.WriteException(e);
            return 1;
        }

        return 0;
    }

    private static void OnFileGenerated(object? _, NinjadogContentFile e)
    {
        AnsiConsole.MarkupLine($"- File generated: [green]{e.OutputPath}[/] with a length of [green]{e.Content.Length}[/] characters.");
    }
}
