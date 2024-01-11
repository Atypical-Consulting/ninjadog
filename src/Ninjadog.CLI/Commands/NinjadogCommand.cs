// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using Ninjadog.Engine;
using Ninjadog.Engine.Collections;
using Ninjadog.Engine.Configuration;
using Ninjadog.Templates.CrudWebAPI.Setup;
using Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;
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
        var templateManifest = new CrudTemplateManifest();
        var todoAppSettings = new TodoAppSettings();

        var outputProcessors = new OutputProcessorCollection(
            settings.InMemory,
            settings.Disk);

        var configuration = new NinjadogEngineConfiguration(
            templateManifest,
            todoAppSettings,
            outputProcessors);

        NinjadogEngineFactory
            .CreateNinjadogEngine(configuration)
            .Run();

        return 0;
    }
}
