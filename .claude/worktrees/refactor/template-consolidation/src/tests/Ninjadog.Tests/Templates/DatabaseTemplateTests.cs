using Ninjadog.Templates.CrudWebAPI.Template.Database;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class DatabaseTemplateTests
{
    [Fact]
    public Task DbConnectionFactory_ProducesCorrectOutput()
    {
        var template = new DbConnectionFactoryTemplate();
        var settings = new TestSettings();
        var result = template.GenerateOne(settings);
        return Verify(result.Content);
    }
}
