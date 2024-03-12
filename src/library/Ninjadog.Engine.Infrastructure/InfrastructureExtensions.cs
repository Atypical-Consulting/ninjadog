// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.Engine.Infrastructure.OutputProcessors;
using Ninjadog.Engine.Infrastructure.Services;

namespace Ninjadog.Engine.Infrastructure;

/// <summary>
/// Provides extension methods for configuring the Ninjadog Engine infrastructure.
/// </summary>
public static class InfrastructureExtensions
{
    /// <summary>
    /// Adds the Ninjadog Engine infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<ICliDotnetService, CliDotnetService>();
        services.AddSingleton<IDiskOutputProcessor, DiskOutputProcessor>();
        services.AddSingleton<IInMemoryOutputProcessor, InMemoryOutputProcessor>();
        services.AddSingleton<INinjadogAppService, NinjadogAppService>();

        return services;
    }
}
