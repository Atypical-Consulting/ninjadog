// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Ninjadog.Settings.Extensions;

namespace Ninjadog.CLI.Commands;

internal sealed class InitCommand
    : Command<InitCommandSettings>
{
    public override int Execute(CommandContext context, InitCommandSettings settings, CancellationToken cancellationToken)
    {
        try
        {
            NinjadogInitialSettings initialSettings = new("MyNinjadogApp");

            // parse to JSON
            var json = initialSettings.ToJsonString();

            // write to file
            File.WriteAllText("ninjadog.json", json);

            // notify user
            WriteLine("Ninjadog settings file created successfully.");

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
