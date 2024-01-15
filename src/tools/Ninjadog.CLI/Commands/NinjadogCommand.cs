// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.ComponentModel;
using Ninjadog.Engine;
using Ninjadog.Engine.Collections;
using Ninjadog.Engine.Configuration;
using Ninjadog.Engine.EventArgs;
using Ninjadog.Settings;
using Ninjadog.Templates;
using Spectre.Console;
using Spectre.Console.Cli;
using static Ninjadog.CLI.Utilities.SpectreWriteHelpers;
using static Spectre.Console.AnsiConsole;

namespace Ninjadog.CLI.Commands;

internal sealed class NinjadogCommandSettings : CommandSettings
{
    [CommandOption("-i|--in-memory")]
    [DefaultValue(true)]
    public bool InMemory { get; init; }

    [CommandOption("-d|--disk")]
    [DefaultValue(true)]
    public bool Disk { get; init; }
}

internal sealed class NinjadogCommand : Command<NinjadogCommandSettings>
{
    private readonly NinjadogTemplateManifest _templateManifest;
    private readonly NinjadogSettings _settings;

    public NinjadogCommand(NinjadogTemplateManifest templateManifest, NinjadogSettings settings)
    {
        _templateManifest =
            templateManifest
            ?? throw new ArgumentNullException(nameof(templateManifest));

        _settings =
            settings
            ?? throw new ArgumentNullException(nameof(settings));
    }

    public override int Execute(CommandContext context, NinjadogCommandSettings settings)
    {
        MarkupLine("[bold]Using the following settings:[/]");
        WriteSettingsTable(table => table
            .AddRow("Engine", $"[green]Ninjadog.Engine[/] v2.0.0-alpha")
            .AddRow("Template", $"[green]{_templateManifest.Name}[/] v{_templateManifest.Version}")
            .AddRow("App name", $"[green]{_settings.Config.Name}[/] v{_settings.Config.Version} with [green]{_settings.Entities.Count}[/] entities")
            .AddRow("App entities", $"[green]{string.Join(", ", _settings.Entities.Keys)}[/]")
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
            .AddRow("Setup .NET Aspire", IsEnableMarkup(false))
            .AddRow("Create Dockerfile", IsEnableMarkup(true))
            .AddRow("Create Git repository", IsEnableMarkup(false))
            .AddRow("Create GitHub Actions", IsEnableMarkup(false))
            .AddRow("Push on GitHub", IsEnableMarkup(false)));

        WriteLine();
        MarkupLine("[bold]Building the Ninjadog Engine...[/]");

        var outputProcessors = new OutputProcessorCollection(settings.InMemory, settings.Disk);
        var engineConfiguration = new NinjadogEngineConfiguration(_templateManifest, _settings, outputProcessors);
        var ninjadogEngine = NinjadogEngineFactory.CreateNinjadogEngine(engineConfiguration);

        ninjadogEngine.OnAfterContentProcessed += OnAfterContentProcessed;
        ninjadogEngine.OnDotnetVersionChecked += OnDotnetVersionChecked;
        ninjadogEngine.OnRunCompleted += OnRunCompleted;
        ninjadogEngine.OnShutdown += OnShutdown;

        try
        {
            WriteLine();
            MarkupLine("[bold]Generating files...[/]");
            ninjadogEngine.Run();
        }
        catch (Exception e)
        {
            WriteLine();
            WriteException(e);
            return 1;
        }

        return 0;
    }

    private static string IsEnableMarkup(bool enabled)
    {
        return enabled ? "[green]enabled[/]" : "[yellow]disabled[/]";
    }

    private static void OnDotnetVersionChecked(object? _, Version version)
    {
        MarkupLine($"- .NET CLI version: [green]{version}[/] detected.");
    }

    private static void OnAfterContentProcessed(object? _, NinjadogContentEventArgs e)
    {
        var outputPath = e.ContentFile.OutputPath;
        var length = e.ContentFile.Length;

        Write("- File generated: ");
        WriteTextPath(outputPath);
        Markup($" with a length of [green]{length:N0}[/] characters.");
        WriteLine();
    }

    private static void OnRunCompleted(object? _, NinjadogEngineRunEventArgs e)
    {
        WriteLine();
        MarkupLine("[bold]Ninjadog Engine run summary:[/]");
        MarkupLine($"- Run completed in [green]{e.RunTime:g}[/] seconds");
        MarkupLine($"- Errors encountered: [green]{e.Exceptions.Count:N0}[/] errors");
        MarkupLine($"- Total files generated: [green]{e.TotalFilesGenerated:N0}[/] files");
        MarkupLine($"- Total characters generated in files: [green]{e.TotalCharactersGenerated:N0}[/] characters");
        MarkupLine($"  - It represents ~[green]{e.TotalCharactersGenerated / 5}[/] words or ~[green]{e.TotalCharactersGenerated / 150}[/] minutes saved");
    }

    private static void OnShutdown(object? _, EventArgs e)
    {
        WriteLine();
        MarkupLine("[bold]Ninjadog Engine shutting down.[/]");
        MarkupLine("[bold]Have a great day![/]");
    }
}
