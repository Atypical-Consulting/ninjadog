// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;
using Ninjadog.Engine.Core.OutputProcessors;

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Represents a collection of output processors. This class extends a list of IOutputProcessor
/// and provides a convenient way to manage multiple output processors, including in-memory and disk-based processors.
/// </summary>
public class NinjadogOutputProcessors : List<IOutputProcessor>
{
    /// <summary>
    /// Initializes a new instance of the OutputProcessorCollection with optional in-memory and disk output processors.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use for dependency injection.</param>
    /// <param name="inMemory">Indicates whether to include an in-memory output processor.</param>
    /// <param name="disk">Indicates whether to include a disk output processor.</param>
    public NinjadogOutputProcessors(
        IServiceProvider serviceProvider,
        bool inMemory = true,
        bool disk = true)
    {
        if (inMemory)
        {
            var outputProcessor = serviceProvider.GetRequiredService<IInMemoryOutputProcessor>();
            Add(outputProcessor);
        }

        if (disk)
        {
            var outputProcessor = serviceProvider.GetRequiredService<IDiskOutputProcessor>();
            Add(outputProcessor);
        }
    }
}
