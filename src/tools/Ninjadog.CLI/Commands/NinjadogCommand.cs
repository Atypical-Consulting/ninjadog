// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Ninjadog.CLI.Utilities;

namespace Ninjadog.CLI.Commands;

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
