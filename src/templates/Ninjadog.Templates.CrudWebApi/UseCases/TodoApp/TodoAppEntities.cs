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
        NinjadogEntityProperties todoListProperties = new()
        {
            { "Id", new NinjadogEntityId() },
            { "Title", new NinjadogEntityProperty<string>() },
        };

        NinjadogEntityRelationships todoListRelationships = new()
        {
            { "Items", new NinjadogEntityRelationship(TodoItem, NinjadogEntityRelationshipType.OneToMany) },
            { "Categories", new NinjadogEntityRelationship(TodoCategory, NinjadogEntityRelationshipType.OneToMany) },
        };

        Add(TodoList, new NinjadogEntity(todoListProperties, todoListRelationships));

        Add(TodoItem, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Description", new NinjadogEntityProperty<string>() },
                { "IsCompleted", new NinjadogEntityProperty<bool>() },
                { "DueDate", new NinjadogEntityProperty<DateTime>() },
            }));

        Add(TodoCategory, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "Id", new NinjadogEntityId() },
                { "Name", new NinjadogEntityProperty<string>() },
                { "Color", new NinjadogEntityProperty<string>() },
                { "Icon", new NinjadogEntityProperty<string>() },
            }));
    }
}
