using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;

namespace Ninjadog.Templates.CrudWebAPI.Setup;

// this class is used to define the template files that will be used by the template engine

// we could use a template with different destination paths.

public class CrudTemplateFiles : NinjadogTemplateFiles
{
    public CrudTemplateFiles()
    {
        Add(new DtoTemplate());
    }
}
