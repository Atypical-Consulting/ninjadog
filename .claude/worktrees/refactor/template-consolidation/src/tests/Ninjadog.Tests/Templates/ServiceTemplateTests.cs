using Ninjadog.Templates.CrudWebAPI.Template.Repositories;
using Ninjadog.Templates.CrudWebAPI.Template.Services;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class ServiceTemplateTests
{
    [Fact]
    public Task Service_WithGuidKeyEntity_ProducesCorrectService()
    {
        var template = new ServiceTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task ServiceInterface_WithGuidKeyEntity_ProducesCorrectInterface()
    {
        var template = new ServiceInterfaceTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task RepositoryInterface_WithGuidKeyEntity_ProducesCorrectInterface()
    {
        var template = new RepositoryInterfaceTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task Service_WithIntKeyEntity_ProducesCorrectService()
    {
        var template = new ServiceTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task RepositoryInterface_WithIntKeyEntity_ProducesCorrectInterface()
    {
        var template = new RepositoryInterfaceTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }
}
