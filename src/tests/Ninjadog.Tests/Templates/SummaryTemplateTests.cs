using Ninjadog.Templates.CrudWebAPI.Template.Summaries;
using Ninjadog.Tests.Helpers;

namespace Ninjadog.Tests.Templates;

public partial class SummaryTemplateTests
{
    [Fact]
    public Task CreateSummary_WithGuidKeyEntity_ProducesCorrectSummary()
    {
        var template = new CreateSummaryTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task DeleteSummary_WithGuidKeyEntity_ProducesCorrectSummary()
    {
        var template = new DeleteSummaryTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetAllSummary_WithGuidKeyEntity_ProducesCorrectSummary()
    {
        var template = new GetAllSummaryTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task GetSummary_WithGuidKeyEntity_ProducesCorrectSummary()
    {
        var template = new GetSummaryTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }

    [Fact]
    public Task UpdateSummary_WithGuidKeyEntity_ProducesCorrectSummary()
    {
        var template = new UpdateSummaryTemplate();
        var entity = TestEntities.CreateGuidKeyEntity();
        var result = template.GenerateOneByEntity(entity, "TestApp.Api");
        return Verify(result.Content);
    }
}
