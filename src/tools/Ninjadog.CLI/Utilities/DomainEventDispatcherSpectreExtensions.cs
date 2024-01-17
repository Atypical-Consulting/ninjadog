// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using static Ninjadog.CLI.Utilities.SpectreWriteHelpers;

namespace Ninjadog.CLI.Utilities;

internal static class DomainEventDispatcherSpectreExtensions
{
    public static void RegisterAllHandlers(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterOnBeforeEngineRun();
        dispatcher.RegisterOnAfterEngineRun();
        dispatcher.RegisterOnBeforeTemplateGenerated();
        dispatcher.RegisterOnAfterTemplateGenerated();
        dispatcher.RegisterOnAfterContentGenerated();
        dispatcher.RegisterOnErrorOccurred();
    }

    private static void RegisterOnBeforeEngineRun(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterHandler((BeforeEngineRunEvent e) =>
        {
            var (config, entities) = e.Settings;
            var manifest = e.TemplateManifest;

            MarkupLine("[bold]Using the following settings:[/]");
            WriteSettingsTable(table => table
                .AddRow("Engine", $"[green]Ninjadog.Engine[/] v2.0.0-alpha")
                .AddRow("Template", $"[green]{manifest.Name}[/] v{manifest.Version}")
                .AddRow("App name", $"[green]{config.Name}[/] v{config.Version} with [green]{entities.Count}[/] entities")
                .AddRow("App entities", $"[green]{string.Join(", ", entities.Keys)}[/]")
                .AddRow("Authentication", IsEnableMarkup(false))
                .AddRow("Persistence", "[green]SQLite[/]"));

            WriteLine();
            MarkupLine("[bold]Output processors:[/]");
            WriteSettingsTable(table => table
                .AddRow("InMemory", IsEnableMarkup(true))
                .AddRow("Disk", IsEnableMarkup(true))
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
            WriteLine();
            MarkupLine("[bold]Generating files...[/]");
            return Task.CompletedTask;
        });
    }

    private static void RegisterOnAfterEngineRun(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterHandler((AfterEngineRunEvent e) =>
        {
            var elapsed = e.ContextSnapshot.TotalTimeElapsed;
            var totalFilesGenerated = e.ContextSnapshot.TotalFilesGenerated;
            var totalCharactersGenerated = e.ContextSnapshot.TotalCharactersGenerated;

            WriteLine();
            MarkupLine("[bold]Ninjadog Engine run summary:[/]");
            MarkupLine($"- Run completed in [green]{elapsed:g}[/] seconds");
            MarkupLine($"- Total files generated: [green]{totalFilesGenerated:N0}[/] files");
            MarkupLine($"- Total characters generated in files: [green]{totalCharactersGenerated:N0}[/] characters");
            MarkupLine($"  - It represents ~[green]{totalCharactersGenerated / 5}[/] words or ~[green]{totalCharactersGenerated / 150}[/] minutes saved");

            WriteLine();
            MarkupLine("[bold]Ninjadog Engine shutting down.[/]");
            MarkupLine("[bold]Have a great day![/]");
            return Task.CompletedTask;
        });
    }

    private static void RegisterOnBeforeTemplateGenerated(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterHandler((BeforeTemplateParsedEvent e) =>
        {
            var templateName = e.Template.Name;
            WriteLine();
            MarkupLine($"- Processing template [yellow]{templateName}[/]...");
            return Task.CompletedTask;
        });
    }

    private static void RegisterOnAfterTemplateGenerated(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterHandler((AfterTemplateParsedEvent e) => Task.CompletedTask);
    }

    private static void RegisterOnAfterContentGenerated(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterHandler((AfterContentGeneratedEvent e) =>
        {
            var outputPath = e.ContentFile.OutputPath;
            var length = e.ContentFile.Length;

            Write("  - File generated: ");
            WriteTextPath(outputPath);
            Markup($" with a length of [green]{length:N0}[/] characters.");
            WriteLine();
            return Task.CompletedTask;
        });
    }

    private static void RegisterOnErrorOccurred(this IDomainEventDispatcher dispatcher)
    {
        dispatcher.RegisterHandler((ErrorOccurredEvent e) =>
        {
            WriteLine();
            MarkupLine("[bold red]Error occurred:[/]");
            WriteException(e.Exception);
            return Task.CompletedTask;
        });
    }

    private static string IsEnableMarkup(bool enabled)
    {
        return enabled ? "[green]enabled[/]" : "[yellow]disabled[/]";
    }
}
