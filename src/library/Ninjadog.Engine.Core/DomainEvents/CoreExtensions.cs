// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;

namespace Ninjadog.Engine.Core.DomainEvents;

/// <summary>
/// Provides extension methods for registering domain event dispatching services.
/// </summary>
public static class CoreExtensions
{
    /// <summary>
    /// Adds domain event dispatcher and associated handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddTransient<IDomainEventProcessor<BeforeEngineRunEvent>, BeforeEngineRunProcessor>();
        services.AddTransient<IDomainEventProcessor<AfterEngineRunEvent>, AfterEngineRunProcessor>();
        services.AddTransient<IDomainEventProcessor<BeforeTemplateParsedEvent>, BeforeTemplateParsedProcessor>();
        services.AddTransient<IDomainEventProcessor<AfterTemplateParsedEvent>, AfterTemplateParsedProcessor>();
        services.AddTransient<IDomainEventProcessor<BeforeContentGeneratedEvent>, BeforeContentGeneratedProcessor>();
        services.AddTransient<IDomainEventProcessor<AfterContentGeneratedEvent>, AfterContentGeneratedProcessor>();
        services.AddTransient<IDomainEventProcessor<ErrorOccurredEvent>, ErrorOccurredProcessor>();

        return services;
    }
}
