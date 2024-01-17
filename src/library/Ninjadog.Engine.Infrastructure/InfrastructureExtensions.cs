// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.Engine.Core.Abstractions;
using Ninjadog.Engine.Core.OutputProcessors;
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
        return services;
    }
}
