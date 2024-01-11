// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Templates;
using Ninjadog.Templates.CrudWebAPI.Setup;
using Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;
using Spectre.Console.Cli;

namespace Ninjadog.CLI.Commands;

internal sealed class NinjadogCommand : Command<NinjadogCommand.Settings>
{
    public sealed class Settings : CommandSettings;

    public override int Execute(CommandContext context, Settings settings)
    {
        var templateManifest = new CrudTemplateManifest();
        var todoAppSettings = new TodoAppSettings();

        var engine = new NinjadogTemplateEngine(templateManifest, todoAppSettings);
        engine.Run();

        return 0;
    }
}
