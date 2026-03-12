using Ninjadog.Settings.Schema;

namespace Ninjadog.CLI.Commands;

internal sealed class UpdateCommand
    : Command<UpdateCommandSettings>
{
    public override int Execute(CommandContext context, UpdateCommandSettings settings, CancellationToken cancellationToken)
    {
        try
        {
            File.WriteAllText("ninjadog.schema.json", SchemaProvider.GetSchemaText());

            MarkupLine("[green]Schema file updated successfully.[/]");
            MarkupLine("[dim]  -> ninjadog.schema.json[/]");

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
