// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.SaaS.Infrastructure.Adapters;
using Ninjadog.SaaS.Infrastructure.Ports;

namespace Ninjadog.SaaS.Infrastructure;

/// <summary>
/// Dependency injection configuration.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>A reference to the service collection.</returns>
    public static IServiceCollection AddSaaSInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IGitHubRepositoryService, GitHubRepositoryService>();

        return services;
    }
}
