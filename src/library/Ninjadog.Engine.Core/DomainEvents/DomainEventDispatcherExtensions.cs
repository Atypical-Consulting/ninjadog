// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.Engine.Core.Abstractions;

namespace Ninjadog.Engine.Core.DomainEvents;

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
        services.AddTransient<IDomainEventHandler<BeforeEngineRunEvent>, BeforeEngineRunHandler>();
        services.AddTransient<IDomainEventHandler<AfterEngineRunEvent>, AfterEngineRunHandler>();
        services.AddTransient<IDomainEventHandler<BeforeTemplateProcessedEvent>, BeforeTemplateProcessedHandler>();
        services.AddTransient<IDomainEventHandler<AfterTemplateProcessedEvent>, AfterTemplateProcessedHandler>();
        services.AddTransient<IDomainEventHandler<BeforeContentProcessedEvent>, BeforeContentProcessedHandler>();
        services.AddTransient<IDomainEventHandler<AfterContentProcessedEvent>, AfterContentProcessedHandler>();
        services.AddTransient<IDomainEventHandler<ErrorOccurredEvent>, ErrorOccurredHandler>();

        return services;
    }
}
