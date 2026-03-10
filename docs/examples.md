---
title: Generated Examples
layout: default
nav_order: 6
---

# Generated Output Examples

All examples below are **real generated code** from Ninjadog's verified snapshot tests.

## Endpoint -- GetAll with pagination

```csharp
public partial class GetAllTodoItemsEndpoint
    : EndpointWithoutRequest<GetAllTodoItemsResponse>
{
    public ITodoItemService TodoItemService { get; private set; } = null!;

    public override void Configure()
    {
        Get("/todo-items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var page = int.TryParse(HttpContext.Request.Query["page"], out var p) && p > 0 ? p : 1;
        var pageSize = int.TryParse(HttpContext.Request.Query["pageSize"], out var ps) && ps > 0 ? ps : 10;

        var (todoItems, totalCount) = await TodoItemService.GetAllAsync(page, pageSize);
        var todoItemsResponse = todoItems.ToTodoItemsResponse(page, pageSize, totalCount);
        await SendOkAsync(todoItemsResponse, ct);
    }
}
```

## Endpoint -- GetOne with route constraint

```csharp
public partial class GetTodoItemEndpoint
    : Endpoint<GetTodoItemRequest, TodoItemResponse>
{
    public ITodoItemService TodoItemService { get; private set; } = null!;

    public override void Configure()
    {
        Get("/todo-items/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTodoItemRequest req, CancellationToken ct)
    {
        var todoItem = await TodoItemService.GetAsync(req.Id);

        if (todoItem is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var todoItemResponse = todoItem.ToTodoItemResponse();
        await SendOkAsync(todoItemResponse, ct);
    }
}
```

## Validator -- type-aware (skips value types)

```csharp
public partial class CreateTodoItemRequestValidator : Validator<CreateTodoItemRequest>
{
    public CreateTodoItemRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required!");

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage("DueDate is required!");

        // IsCompleted (bool), Priority (int), Cost (decimal) -- skipped, value types always have defaults
    }
}
```

## Database -- SQLite schema with type-aware columns

```csharp
public partial class DatabaseInitializer(IDbConnectionFactory connectionFactory)
{
    public async Task InitializeAsync()
    {
        using var connection = await connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS TodoItems (
            Id CHAR(36) PRIMARY KEY,
            Title TEXT NOT NULL,
            Description TEXT NOT NULL,
            IsCompleted INTEGER NOT NULL,
            DueDate TEXT NOT NULL,
            Priority INTEGER NOT NULL,
            Cost REAL NOT NULL)");
    }
}
```
