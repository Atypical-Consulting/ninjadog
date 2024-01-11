namespace Ninjadog.Templates;

public interface ITemplateEngine
{
    Dictionary<string, string> GeneratedFiles { get; }
    void Run();
}
