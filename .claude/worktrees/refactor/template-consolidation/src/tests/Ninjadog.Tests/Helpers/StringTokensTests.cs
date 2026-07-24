using Ninjadog.Helpers;

namespace Ninjadog.Tests.Helpers;

public class StringTokensTests
{
    private readonly StringTokens _tokens = new("TodoItem");

    [Fact]
    public void Model_ReturnsPascalName()
    {
        Assert.Equal("TodoItem", _tokens.Model);
    }

    [Fact]
    public void ModelHumanized_ReturnsLowercaseHumanized()
    {
        Assert.Equal("todo item", _tokens.ModelHumanized);
    }

    [Fact]
    public void ModelEndpoint_ReturnsDashedPluralWithSlash()
    {
        Assert.Equal("/todo-items", _tokens.ModelEndpoint);
    }

    [Fact]
    public void VarModel_ReturnsCamelCase()
    {
        Assert.Equal("todoItem", _tokens.VarModel);
    }

    [Fact]
    public void VarExistingModel_ReturnsPrefixedPascal()
    {
        Assert.Equal("existingTodoItem", _tokens.VarExistingModel);
    }

    [Fact]
    public void Models_ReturnsPluralizedPascal()
    {
        Assert.Equal("TodoItems", _tokens.Models);
    }

    [Fact]
    public void ModelsHumanized_ReturnsLowercasePluralHumanized()
    {
        Assert.Equal("todo items", _tokens.ModelsHumanized);
    }

    [Fact]
    public void VarModels_ReturnsPluralizedCamelCase()
    {
        Assert.Equal("todoItems", _tokens.VarModels);
    }

    [Fact]
    public void ClassModelDto_ReturnsPascalWithDtoSuffix()
    {
        Assert.Equal("TodoItemDto", _tokens.ClassModelDto);
    }

    [Fact]
    public void VarModelDto_ReturnsCamelWithDtoSuffix()
    {
        Assert.Equal("todoItemDto", _tokens.VarModelDto);
    }

    [Fact]
    public void InterfaceModelRepository_ReturnsIPrefix()
    {
        Assert.Equal("ITodoItemRepository", _tokens.InterfaceModelRepository);
    }

    [Fact]
    public void ClassModelRepository_ReturnsPascalWithRepositorySuffix()
    {
        Assert.Equal("TodoItemRepository", _tokens.ClassModelRepository);
    }

    [Fact]
    public void FieldModelRepository_ReturnsUnderscoreCamelPrefix()
    {
        Assert.Equal("_todoItemRepository", _tokens.FieldModelRepository);
    }

    [Fact]
    public void InterfaceModelService_ReturnsIPrefix()
    {
        Assert.Equal("ITodoItemService", _tokens.InterfaceModelService);
    }

    [Fact]
    public void ClassModelService_ReturnsPascalWithServiceSuffix()
    {
        Assert.Equal("TodoItemService", _tokens.ClassModelService);
    }

    [Fact]
    public void ClassGetAllModelsEndpoint_ReturnsCorrectFormat()
    {
        Assert.Equal("GetAllTodoItemsEndpoint", _tokens.ClassGetAllModelsEndpoint);
    }

    [Fact]
    public void ClassGetModelRequest_ReturnsCorrectFormat()
    {
        Assert.Equal("GetTodoItemRequest", _tokens.ClassGetModelRequest);
    }

    [Fact]
    public void ClassCreateModelRequest_ReturnsCorrectFormat()
    {
        Assert.Equal("CreateTodoItemRequest", _tokens.ClassCreateModelRequest);
    }

    [Fact]
    public void ClassUpdateModelRequest_ReturnsCorrectFormat()
    {
        Assert.Equal("UpdateTodoItemRequest", _tokens.ClassUpdateModelRequest);
    }

    [Fact]
    public void ClassDeleteModelRequest_ReturnsCorrectFormat()
    {
        Assert.Equal("DeleteTodoItemRequest", _tokens.ClassDeleteModelRequest);
    }

    [Fact]
    public void ClassCreateModelRequestValidator_ReturnsCorrectFormat()
    {
        Assert.Equal("CreateTodoItemRequestValidator", _tokens.ClassCreateModelRequestValidator);
    }

    [Fact]
    public void ClassUpdateModelRequestValidator_ReturnsCorrectFormat()
    {
        Assert.Equal("UpdateTodoItemRequestValidator", _tokens.ClassUpdateModelRequestValidator);
    }

    [Fact]
    public void ClassModelResponse_ReturnsCorrectFormat()
    {
        Assert.Equal("TodoItemResponse", _tokens.ClassModelResponse);
    }

    [Fact]
    public void ClassGetAllModelsResponse_ReturnsCorrectFormat()
    {
        Assert.Equal("GetAllTodoItemsResponse", _tokens.ClassGetAllModelsResponse);
    }

    [Fact]
    public void MethodToModel_ReturnsCorrectFormat()
    {
        Assert.Equal("ToTodoItem", _tokens.MethodToModel);
    }

    [Fact]
    public void MethodToModelDto_ReturnsCorrectFormat()
    {
        Assert.Equal("ToTodoItemDto", _tokens.MethodToModelDto);
    }

    [Fact]
    public void MethodToModelResponse_ReturnsCorrectFormat()
    {
        Assert.Equal("ToTodoItemResponse", _tokens.MethodToModelResponse);
    }

    [Fact]
    public void MethodToModelsResponse_ReturnsPluralizedFormat()
    {
        Assert.Equal("ToTodoItemsResponse", _tokens.MethodToModelsResponse);
    }

    [Fact]
    public void ClassGetAllModelsSummary_ReturnsCorrectFormat()
    {
        Assert.Equal("GetAllTodoItemsSummary", _tokens.ClassGetAllModelsSummary);
    }

    [Theory]
    [InlineData("Product", "/products")]
    [InlineData("Category", "/categories")]
    [InlineData("Person", "/people")]
    [InlineData("Order", "/orders")]
    public void ModelEndpoint_DifferentEntities_ProducesCorrectRoutes(string pascal, string expectedEndpoint)
    {
        var tokens = new StringTokens(pascal);
        Assert.Equal(expectedEndpoint, tokens.ModelEndpoint);
    }
}
