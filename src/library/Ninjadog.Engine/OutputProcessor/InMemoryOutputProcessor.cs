// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Abstractions;

namespace Ninjadog.Engine.OutputProcessor;

/// <summary>
/// An output processor that stores generated content in memory.
/// This class implements the IOutputProcessor interface and provides a mechanism for storing
/// output content within an internal memory structure.
/// </summary>
public class InMemoryOutputProcessor : IOutputProcessor
{
    /// <summary>
    /// Gets the memory storage that holds the processed content.
    /// </summary>
    public List<string> MemoryStorage { get; } = [];

    /// <summary>
    /// Processes the given content by storing it in memory.
    /// </summary>
    /// <param name="content">The content to be processed and stored.</param>
    public void ProcessOutput(string? content)
    {
        if (content != null)
        {
            MemoryStorage.Add(content);
        }
    }
}
