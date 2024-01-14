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

    private int _totalFilesGenerated;
    private int _totalCharactersGenerated;

    public override int Execute(CommandContext context, Settings settings)
    {
        CrudTemplateManifest templateManifest = new();
        TodoAppSettings todoAppSettings = new();
        OutputProcessorCollection outputProcessors = new(settings.InMemory, settings.Disk);
        NinjadogEngineConfiguration configuration = new(templateManifest, todoAppSettings, outputProcessors);

        AnsiConsole.MarkupLine("[bold]Using the following settings:[/]");
        AnsiConsole.MarkupLine($"- App name         : [green]{todoAppSettings.Config.Name}[/] v{todoAppSettings.Config.Version} with [green]{todoAppSettings.Entities.Count}[/] entities");
        AnsiConsole.MarkupLine($"- App entities     : [green]{string.Join(", ", todoAppSettings.Entities.Keys)}[/]");
        AnsiConsole.MarkupLine($"- Template         : [green]{templateManifest.Name}[/] v{templateManifest.Version}");
        AnsiConsole.MarkupLine($"- Authentification : [yellow]False[/]");
        AnsiConsole.MarkupLine($"- Persistence      : [green]SQLite[/]");

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold]Output processors:[/]");
        AnsiConsole.MarkupLine($"- InMemory         : [green]{settings.InMemory}[/]");
        AnsiConsole.MarkupLine($"- Disk             : [green]{settings.Disk}[/]");
        AnsiConsole.MarkupLine($"- Zip              : [yellow]False[/]");

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold]Git integration:[/]");
        AnsiConsole.MarkupLine($"- Git repository   : [yellow]False[/]");
        AnsiConsole.MarkupLine($"- GitHub Actions   : [yellow]False[/]");
        AnsiConsole.MarkupLine($"- Push on GitHub   : [yellow]False[/]");


        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold]Building the Ninjadog Engine...[/]");
        var ninjadogEngine = NinjadogEngineFactory.CreateNinjadogEngine(configuration);
        ninjadogEngine.FileGenerated += OnFileGenerated;
        ninjadogEngine.DotnetVersionChecked += OnDotnetVersionChecked;

        try
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold]Generating files...[/]");
            ninjadogEngine.Run();

            // TODO: Add a summary of the run:
            // Ninjadog Engine run summary:
            // - Total files generated: 10
            //     - Total time elapsed: 10 minutes
            //     - Errors encountered: 0
            // AnsiConsole.MarkupLine("[bold]Ninjadog Engine run summary:[/]");
            // AnsiConsole.MarkupLine($"- Total files generated: [green]{ninjadogEngine.TotalFilesGenerated}[/]");
            // AnsiConsole.MarkupLine($"- Total time elapsed: [green]{ninjadogEngine.TotalTimeElapsed}[/]");
            // AnsiConsole.MarkupLine($"- Errors encountered: [green]{ninjadogEngine.ErrorsEncountered}[/]");

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold]Ninjadog Engine run summary:[/]");
            AnsiConsole.MarkupLine($"- Total files generated: [green]{_totalFilesGenerated}[/] files");
            AnsiConsole.MarkupLine($"- Total characters generated in files: [green]{_totalCharactersGenerated}[/] characters");
            AnsiConsole.MarkupLine($"  - It represents ~[green]{_totalCharactersGenerated / 5}[/] words or ~[green]{_totalCharactersGenerated / 150}[/] minutes saved");

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold]Ninjadog Engine shutting down.[/]");
            AnsiConsole.MarkupLine("[bold]Have a great day![/]");
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.WriteException(e);
            return 1;
        }

        return 0;
    }

    private void OnFileGenerated(object? _, NinjadogContentFile e)
    {
        _totalFilesGenerated++;
        _totalCharactersGenerated += e.Content.Length;
        AnsiConsole.MarkupLine($"- File generated: [green]{e.OutputPath}[/] with a length of [green]{e.Content.Length}[/] characters.");
    }

    private void OnDotnetVersionChecked(object? _, Version version)
    {
        AnsiConsole.MarkupLine($"- .NET CLI version: [green]{version}[/] detected.");
    }
}
