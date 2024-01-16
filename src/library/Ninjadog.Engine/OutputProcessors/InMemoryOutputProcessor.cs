// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.OutputProcessors;

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
    public Dictionary<string, string> MemoryStorage { get; } = [];

    /// <summary>
    /// Processes the given content by storing it in memory.
    /// </summary>
    /// <param name="contentFile">The content file to be processed and stored.</param>
    public void ProcessOutput(NinjadogContentFile contentFile)
    {
        // raise an exception if the output path is already present in the memory storage
        if (MemoryStorage.ContainsKey(contentFile.OutputPath))
        {
            throw new InvalidOperationException(
                $"The output path {contentFile.OutputPath} is already present in the memory storage.");
        }

        MemoryStorage[contentFile.OutputPath] = contentFile.Content;
    }
}
