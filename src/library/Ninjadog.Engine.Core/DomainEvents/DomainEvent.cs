// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Represents a base interface for domain events.
/// </summary>
public interface IDomainEvent;

/// <summary>
/// Abstract base class for domain events, providing common properties and functionality.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <summary>
    /// Gets the date and time when the event occurred.
    /// </summary>
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents an event that is triggered before a template is processed.
/// </summary>
/// <param name="Template">The template that is about to be processed.</param>
public record BeforeTemplateProcessedEvent(NinjadogTemplate Template) : DomainEvent;

/// <summary>
/// Represents an event that is triggered after a template has been processed.
/// </summary>
/// <param name="Template">The template that has been processed.</param>
public record AfterTemplateProcessedEvent(NinjadogTemplate Template) : DomainEvent;

/// <summary>
/// Defines the interface for a domain event dispatcher.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches the specified domain event to its respective handlers.
    /// </summary>
    /// <param name="domainEvent">The domain event to dispatch.</param>
    /// <returns>A task that represents the asynchronous dispatch operation.</returns>
    Task Dispatch(IDomainEvent domainEvent);
}

/// <summary>
/// Provides an implementation for dispatching domain events.
/// </summary>
/// <param name="serviceProvider">The service provider used for resolving event handlers.</param>
/// <remarks>
/// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
/// </remarks>
public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches the specified domain event to its respective handlers.
    /// </summary>
    /// <param name="domainEvent">The domain event to dispatch.</param>
    /// <returns>A task that represents the asynchronous operation of dispatching the event.</returns>
    public async Task Dispatch(IDomainEvent domainEvent)
    {
        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("Handle");
            if (method is not null)
            {
                await ((Task)method.Invoke(handler, [domainEvent])!).ConfigureAwait(false);
            }
        }
    }
}

/// <summary>
/// Handles events that occur before a template is processed.
/// </summary>
public class BeforeTemplateProcessedEventHandler
    : IDomainEventHandler<BeforeTemplateProcessedEvent>
{
    /// <summary>
    /// Handles the logic to be executed when a template is about to be processed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the template to be processed.</param>
    public async Task Handle(BeforeTemplateProcessedEvent domainEvent)
    {
        // Logic for handling the event before a template is processed
        Console.WriteLine($"Processing template {domainEvent.DateOccurred}...");
    }
}

/// <summary>
/// Handles events that occur after a template is processed.
/// </summary>
public class AfterTemplateProcessedEventHandler
    : IDomainEventHandler<AfterTemplateProcessedEvent>
{
    /// <summary>
    /// Handles the logic to be executed after a template has been processed.
    /// </summary>
    /// <param name="domainEvent">The event containing details about the processed template.</param>
    public async Task Handle(AfterTemplateProcessedEvent domainEvent)
    {
        // Logic for handling the event after a template is processed
    }
}

/// <summary>
/// Represents a handler for a specific type of domain event.
/// </summary>
/// <typeparam name="T">The type of the domain event to handle.</typeparam>
public interface IDomainEventHandler<in T>
    where T : DomainEvent
{
    /// <summary>
    /// Handles the given domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Handle(T domainEvent);
}

/// <summary>
/// Provides extension methods for registering domain event dispatching services.
/// </summary>
public static class DomainEventDispatcherExtensions
{
    /// <summary>
    /// Adds domain event dispatcher and associated handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddTransient<IDomainEventHandler<BeforeTemplateProcessedEvent>, BeforeTemplateProcessedEventHandler>();
        services.AddTransient<IDomainEventHandler<AfterTemplateProcessedEvent>, AfterTemplateProcessedEventHandler>();

        return services;
    }
}
