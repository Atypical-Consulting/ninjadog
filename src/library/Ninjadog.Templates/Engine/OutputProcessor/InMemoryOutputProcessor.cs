namespace Ninjadog.Templates.Engine.OutputProcessor;

public class InMemoryOutputProcessor : OutputProcessor
{
    public List<string> MemoryStorage { get; } = [];

    public override void ProcessOutput(string? content)
    {
        if (content != null)
        {
            MemoryStorage.Add(content);
        }
    }
}