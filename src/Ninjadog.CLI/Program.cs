using Ninjadog.CLI.Commands;
using Spectre.Console;
using Spectre.Console.Cli;

AnsiConsole.Markup("[underline red]Hello[/] Ninjadog!");

var app = new CommandApp<NinjadogCommand>();
return app.Run(args);
