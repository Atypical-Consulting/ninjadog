// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.CLI.Commands;
using Spectre.Console;
using Spectre.Console.Cli;

AnsiConsole.Write(
    new FigletText("Ninjadog")
        .LeftJustified()
        .Color(Color.Red));
AnsiConsole.MarkupLine("[bold]by Atypical Consulting SRL[/]");
AnsiConsole.WriteLine();

var app = new CommandApp<NinjadogCommand>();
return app.Run(args);
