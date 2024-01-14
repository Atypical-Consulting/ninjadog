// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities;
using Ninjadog.Settings.Entities.Properties;

namespace Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

/// <summary>
/// Defines the entities specific to the TodoApp use case.
/// This class extends <see cref="NinjadogEntities"/> and initializes the entities '<see cref="TodoList"/>' and '<see cref="TodoItem"/>'
/// with their respective properties, reflecting the domain model of the Todo application.
/// </summary>
public sealed class TodoAppEntities : NinjadogEntities
{
    private const string TodoList = "TodoList";
    private const string TodoItem = "TodoItem";
    private const string TodoCategory = "TodoCategory";

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoAppEntities"/> class,
    /// defining the structure and properties of <see cref="TodoList"/>,
    /// <see cref="TodoItem"/>, and <see cref="TodoCategory"/> entities.
    /// </summary>
    public TodoAppEntities()
    {
        Add(TodoList, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Title", new NinjadogEntityProperty(nameof(String)) },
                { "Items", new NinjadogEntityProperty($"List<{TodoItem}>") },
                { "Categories", new NinjadogEntityProperty($"List<{TodoCategory}>") },
                // TODO: Implement the following properties
                // { "Items", new NinjadogEntityRelationship(TodoItem, NinjadogEntityRelationshipType.OneToMany) },
                // { "Categories", new NinjadogEntityRelationship(TodoCategory, NinjadogEntityRelationshipType.OneToMany) }
            }));

        Add(TodoItem, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Description", new NinjadogEntityProperty(nameof(String)) },
                { "IsCompleted", new NinjadogEntityProperty(nameof(Boolean)) },
                { "DueDate", new NinjadogEntityProperty(nameof(DateTime)) }
            }));

        Add(TodoCategory, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Name", new NinjadogEntityProperty(nameof(String)) },
                { "Color", new NinjadogEntityProperty(nameof(String)) },
                { "Icon", new NinjadogEntityProperty(nameof(String)) },
            }));
    }
}
