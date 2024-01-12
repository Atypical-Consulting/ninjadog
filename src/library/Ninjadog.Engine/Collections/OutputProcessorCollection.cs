// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
