// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.ComponentModel;
using Ninjadog.Engine;
using Ninjadog.Engine.Collections;
using Ninjadog.Engine.Configuration;
using Ninjadog.Engine.EventArgs;
using Ninjadog.Templates.CrudWebAPI.Setup;
using Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;
using Spectre.Console;
using Spectre.Console.Cli;
using static Spectre.Console.AnsiConsole;

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

        MarkupLine("[bold]Using the following settings:[/]");
        WriteSettingsTable(table => table
            .AddRow("Engine", $"[green]Ninjadog.Engine[/] v2.0.0-alpha")
            .AddRow("Template", $"[green]{templateManifest.Name}[/] v{templateManifest.Version}")
            .AddRow("App name", $"[green]{todoAppSettings.Config.Name}[/] v{todoAppSettings.Config.Version} with [green]{todoAppSettings.Entities.Count}[/] entities")
            .AddRow("App entities", $"[green]{string.Join(", ", todoAppSettings.Entities.Keys)}[/]")
            .AddRow("Authentication", IsEnableMarkup(false))
            .AddRow("Persistence", "[green]SQLite[/]"));

        WriteLine();
        MarkupLine("[bold]Output processors:[/]");
        WriteSettingsTable(table => table
            .AddRow("InMemory", IsEnableMarkup(settings.InMemory))
            .AddRow("Disk", IsEnableMarkup(settings.Disk))
            .AddRow("Zip", IsEnableMarkup(false)));

        WriteLine();
        MarkupLine("[bold]Integrations:[/]");
        WriteSettingsTable(table => table
            .AddRow("Git repository", IsEnableMarkup(false))
            .AddRow("GitHub Actions", IsEnableMarkup(false))
            .AddRow("Push on GitHub", IsEnableMarkup(false))
            .AddRow(".NET Aspire", IsEnableMarkup(false)));

        WriteLine();
        MarkupLine("[bold]Building the Ninjadog Engine...[/]");
        var ninjadogEngine = NinjadogEngineFactory.CreateNinjadogEngine(configuration);
        ninjadogEngine.OnAfterContentProcessed += OnAfterContentProcessed;
        ninjadogEngine.OnDotnetVersionChecked += OnDotnetVersionChecked;
        ninjadogEngine.OnRunCompleted += OnRunCompleted;
        ninjadogEngine.OnShutdown += OnShutdown;

        try
        {
            WriteLine();
            MarkupLine("[bold]Generating files...[/]");

            _fileTree = new Tree("Generated files");
            Live(_fileTree).AutoClear(false).Start(ctx =>
            {
                ninjadogEngine.Run();
            });
        }
        catch (Exception e)
        {
            WriteLine();
            WriteException(e);
            return 1;
        }

        return 0;
    }

    private static void WriteSettingsTable(Action<Table> action)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("");
        table.AddColumn("");
        table.Columns[0].Width(24);
        table.Columns[1].Width(48);
        table.HideHeaders();
        action(table);
        Write(table);
    }

    private static string IsEnableMarkup(bool enabled)
    {
        return enabled ? "[green]enabled[/]" : "[yellow]disabled[/]";
    }

    private static void OnDotnetVersionChecked(object? _, Version version)
    {
        MarkupLine($"- .NET CLI version: [green]{version}[/] detected.");
    }


    private Tree _fileTree = new("Root");
    // private readonly Dictionary<string, IHasTreeNodes> _directoryNodes = [];
    // private static readonly char[] Separators = ['/', '\\'];

    private void OnAfterContentProcessed(object? _, NinjadogContentEventArgs e)
    {
        var length = e.ContentFile.Length;
        var outputPath = e.ContentFile.OutputPath;

        _totalFilesGenerated++;
        _totalCharactersGenerated += length;

        MarkupLine($"- File generated: [green]{outputPath}[/] with a length of [green]{length}[/] characters.");

        // Add the file to the tree
        // AddFileToTree(outputPath, $"[green]{outputPath}[/] ({length} characters)");
    }

    // private void AddFileToTree(string filePath, string displayText)
    // {
    //     var pathComponents = filePath.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
    //
    //     // Build the full path incrementally and add directory nodes as needed
    //     var currentPath = string.Empty;
    //     IHasTreeNodes currentNode = _fileTree;
    //
    //     for (var i = 0; i < pathComponents.Length - 1; i++) // Exclude the last component as it's the file
    //     {
    //         var component = pathComponents[i];
    //         currentPath = string.IsNullOrEmpty(currentPath) ? component : $"{currentPath}/{component}";
    //
    //         if (!_directoryNodes.TryGetValue(currentPath, out currentNode))
    //         {
    //             currentNode = currentNode.AddNode(component);
    //             _directoryNodes[currentPath] = currentNode;
    //         }
    //     }
    //
    //     // Add the file as a leaf node
    //     currentNode.AddNode(displayText);
    // }

    private void OnRunCompleted(object? _, NinjadogEngineRunEventArgs e)
    {
        // TODO: Add a summary of the run:
        // Ninjadog Engine run summary:
        // - Total files generated: 10
        //     - Total time elapsed: 10 minutes
        //     - Errors encountered: 0
        // AnsiConsole.MarkupLine("[bold]Ninjadog Engine run summary:[/]");
        // AnsiConsole.MarkupLine($"- Total files generated: [green]{ninjadogEngine.TotalFilesGenerated}[/]");
        // AnsiConsole.MarkupLine($"- Total time elapsed: [green]{ninjadogEngine.TotalTimeElapsed}[/]");
        // AnsiConsole.MarkupLine($"- Errors encountered: [green]{ninjadogEngine.ErrorsEncountered}[/]");

        WriteLine();
        MarkupLine("[bold]Ninjadog Engine run summary:[/]");
        MarkupLine($"- Run completed in [green]{e.RunTime}[/].");
        MarkupLine($"- Total files generated: [green]{_totalFilesGenerated}[/] files");
        MarkupLine($"- Total characters generated in files: [green]{_totalCharactersGenerated}[/] characters");
        MarkupLine($"  - It represents ~[green]{_totalCharactersGenerated / 5}[/] words or ~[green]{_totalCharactersGenerated / 150}[/] minutes saved");
    }

    private static void OnShutdown(object? _, EventArgs e)
    {
        WriteLine();
        MarkupLine("[bold]Ninjadog Engine shutting down.[/]");
        MarkupLine("[bold]Have a great day![/]");
    }
}
