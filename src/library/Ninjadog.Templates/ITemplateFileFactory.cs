namespace Ninjadog.Templates;

public interface ITemplateFileFactory
{
    NinjadogTemplate Create(TemplateContext context);
}
