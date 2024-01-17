// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.ComponentModel;
using Ninjadog.CLI.Utilities;

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

internal sealed class NinjadogCommand(
    IServiceProvider serviceProvider,
    INinjadogEngineFactory engineFactory,
    IDomainEventDispatcher domainEventDispatcher,
    NinjadogTemplateManifest templateManifest,
    NinjadogSettings ninjadogSettings)
    : Command<NinjadogCommandSettings>
{
    public override int Execute(CommandContext context, NinjadogCommandSettings settings)
    {
        try
        {
            var engineConfiguration = CreateEngineConfiguration(settings);
            var displayService = new NinjadogEngineEventDisplayService(domainEventDispatcher);
            displayService.RegisterAllHandlers();

            var ninjadogEngine = engineFactory.CreateNinjadogEngine(engineConfiguration);
            ninjadogEngine.Run();

            return 0;
        }
        catch (Exception e)
        {
            WriteLine();
            WriteException(e);

            return 1;
        }
    }

    private NinjadogEngineConfiguration CreateEngineConfiguration(NinjadogCommandSettings settings)
    {
        var inMemory = settings.InMemory;
        var disk = settings.Disk;

        var outputProcessors = new NinjadogOutputProcessors(serviceProvider, inMemory, disk);
        var engineConfiguration = new NinjadogEngineConfiguration(templateManifest, ninjadogSettings, outputProcessors);

        return engineConfiguration;
    }
}
