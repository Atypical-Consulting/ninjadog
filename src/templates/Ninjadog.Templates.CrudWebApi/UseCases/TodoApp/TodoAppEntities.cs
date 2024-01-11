// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

public class TodoAppEntities : NinjadogEntities
{
    private const string TodoItem = "TodoItem";
    private const string TodoList = "TodoList";

    public TodoAppEntities()
    {
        Add(TodoItem, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Description", new NinjadogEntityProperty(nameof(String)) },
                { "IsCompleted", new NinjadogEntityProperty(nameof(Boolean)) }
            }));

        Add(TodoList, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Title", new NinjadogEntityProperty(nameof(String)) },
                { "Items", new NinjadogEntityProperty($"List<{TodoItem}>") }
            }));
    }
}
