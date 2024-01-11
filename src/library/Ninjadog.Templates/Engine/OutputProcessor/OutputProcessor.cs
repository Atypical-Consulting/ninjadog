namespace Ninjadog.Templates.Engine.OutputProcessor;

public abstract class OutputProcessor : IOutputProcessor
{
    public abstract void ProcessOutput(string? content);
}