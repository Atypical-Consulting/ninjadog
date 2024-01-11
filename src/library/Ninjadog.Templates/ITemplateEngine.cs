namespace Ninjadog.Templates;

public interface ITemplateEngine
{
    List<string> GeneratedContents { get; }
    void Run();
}
