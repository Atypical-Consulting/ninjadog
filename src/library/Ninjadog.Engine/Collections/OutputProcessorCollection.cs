// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Engine.Abstractions;
using Ninjadog.Engine.OutputProcessor;

namespace Ninjadog.Engine.Collections;

public class OutputProcessorCollection
    : List<IOutputProcessor>
{
    public OutputProcessorCollection(
        bool inMemory = true,
        bool disk = true)
    {
        if (inMemory)
        {
            Add(new InMemoryOutputProcessor());
        }

        if (disk)
        {
            Add(new DiskOutputProcessor());
        }
    }
}
