using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;
using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;
using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Responses;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class ContractTemplateTests
{
    [Fact]
    public Task Dto_WithGuidKeyEntity_ProducesCorrectDto()
    {
        var template = new DtoTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task Dto_WithIntKeyEntity_ProducesCorrectDto()
    {
        var template = new DtoTemplate();
        var entity = TestEntities.CreateIntKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task CreateRequest_WithGuidKeyEntity_ProducesCorrectContract()
    {
        var template = new CreateRequestTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task UpdateRequest_WithGuidKeyEntity_ProducesCorrectContract()
    {
        var template = new UpdateRequestTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task DeleteRequest_WithGuidKeyEntity_ProducesCorrectContract()
    {
        var template = new DeleteRequestTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetRequest_WithGuidKeyEntity_ProducesCorrectContract()
    {
        var template = new GetRequestTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task Response_WithGuidKeyEntity_ProducesCorrectContract()
    {
        var template = new ResponseTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetAllResponse_WithGuidKeyEntity_ProducesCorrectContract()
    {
        var template = new GetAllResponseTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }
}
