using Ninjadog.Engine.Abstractions;

namespace Ninjadog.Engine.OutputProcessor;

public class InMemoryOutputProcessor : IOutputProcessor
{
    public List<string> MemoryStorage { get; } = [];

    public void ProcessOutput(string? content)
    {
        if (content != null)
        {
            MemoryStorage.Add(content);
        }
    }
}
