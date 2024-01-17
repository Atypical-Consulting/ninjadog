// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.ComponentModel;

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
    NinjadogSettings settings)
    : Command<NinjadogCommandSettings>
{
    private readonly NinjadogTemplateManifest _templateManifest =
        templateManifest
        ?? throw new ArgumentNullException(nameof(templateManifest));

    private readonly NinjadogSettings _settings =
        settings
        ?? throw new ArgumentNullException(nameof(settings));

    public override int Execute(CommandContext context, NinjadogCommandSettings settings)
    {
        try
        {
            domainEventDispatcher.RegisterAllHandlers();

            var outputProcessors = new NinjadogOutputProcessors(serviceProvider);
            var engineConfiguration = new NinjadogEngineConfiguration(_templateManifest, _settings, outputProcessors);
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
}
