using Ninjadog.Settings;

namespace Ninjadog.Templates;

public interface ITemplateFileFactory
{
    NinjadogTemplate Create(NinjadogSettings ninjadogSettings);
}
