using Ninjadog.Templates.CrudWebAPI.Template.Endpoints;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class EndpointTemplateTests
{
    [Fact]
    public Task GetAllEndpoint_WithGuidKeyEntity_IncludesPagination()
    {
        var template = new GetAllEndpointTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetEndpoint_WithGuidKey_UsesGuidRouteConstraint()
    {
        var template = new GetEndpointTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetEndpoint_WithIntKey_UsesIntRouteConstraint()
    {
        var template = new GetEndpointTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task DeleteEndpoint_WithGuidKey_UsesEntityKeyInHandler()
    {
        var template = new DeleteEndpointTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }
}
