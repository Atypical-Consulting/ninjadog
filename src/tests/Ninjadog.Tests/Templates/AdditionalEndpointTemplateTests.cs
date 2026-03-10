using Ninjadog.Templates.CrudWebAPI.Template.Endpoints;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class AdditionalEndpointTemplateTests
{
    [Fact]
    public Task CreateEndpoint_WithGuidKey_ProducesCorrectHandler()
    {
        var template = new CreateEndpointTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task CreateEndpoint_WithIntKey_ProducesCorrectHandler()
    {
        var template = new CreateEndpointTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task UpdateEndpoint_WithGuidKey_ProducesCorrectHandler()
    {
        var template = new UpdateEndpointTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task UpdateEndpoint_WithIntKey_ProducesCorrectHandler()
    {
        var template = new UpdateEndpointTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task DeleteEndpoint_WithIntKey_UsesIntRouteConstraint()
    {
        var template = new DeleteEndpointTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetAllEndpoint_WithIntKeyEntity_IncludesPagination()
    {
        var template = new GetAllEndpointTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }
}
