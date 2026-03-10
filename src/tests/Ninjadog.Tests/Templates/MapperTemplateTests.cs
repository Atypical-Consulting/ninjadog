using Ninjadog.Templates.CrudWebAPI.Template.Mapping;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class MapperTemplateTests
{
    [Fact]
    public Task ApiContractToDomain_WithGuidKeyEntity_ProducesCorrectMapping()
    {
        var template = new ApiContractToDomainMapperTemplate();
        var settings = new TestSettings();
        var result = template.GenerateOne(settings);
        return Verify(result.Content);
    }

    [Fact]
    public Task DomainToApiContract_WithGuidKeyEntity_ProducesCorrectMapping()
    {
        var template = new DomainToApiContractMapperTemplate();
        var settings = new TestSettings();
        var result = template.GenerateOne(settings);
        return Verify(result.Content);
    }

    [Fact]
    public Task DomainToDto_WithGuidKeyEntity_ProducesCorrectMapping()
    {
        var template = new DomainToDtoMapperTemplate();
        var settings = new TestSettings();
        var result = template.GenerateOne(settings);
        return Verify(result.Content);
    }

    [Fact]
    public Task DtoToDomain_WithGuidKeyEntity_ProducesCorrectMapping()
    {
        var template = new DtoToDomainMapperTemplate();
        var settings = new TestSettings();
        var result = template.GenerateOne(settings);
        return Verify(result.Content);
    }
}
