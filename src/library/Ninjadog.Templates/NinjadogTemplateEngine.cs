namespace Ninjadog.Templates;

public class NinjadogTemplateEngine : ITemplateEngine
{


    public void Run()
    {
        TemplateContext context = null;
        ITemplateFileFactory templateFileFactory = null;

        // TODO: maybe we should get the list of template files from the configuration or a template manifest
        NinjadogTemplate[] templateFiles = [];

        foreach (var template in templateFiles)
        {
            string? generateSingleFile = template.GenerateSingleFile(context);
            // TODO: where should we write the file?

            IEnumerable<string?> generateMultipleFiles = template.GenerateMultipleFiles(context);
        }


        var templateFile = templateFileFactory.Create(context);
        var result = templateFile.GenerateCode(context);
    }

}
