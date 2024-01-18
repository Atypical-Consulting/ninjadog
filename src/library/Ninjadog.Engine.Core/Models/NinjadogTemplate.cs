// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Extensions;

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Represents an abstract base class for Ninjadog templates.
/// This class provides the foundational structure for generating template-based code.
/// </summary>
public abstract class NinjadogTemplate
{
    /// <summary>
    /// Gets the name of the template.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Gets or sets the category of the template.
    /// </summary>
    public virtual string? Category { get; set; }

    /// <summary>
    /// Generates code in a single file based on the provided settings.
    /// This method can be overridden by derived classes to implement specific logic
    /// for one-to-one code generation.
    /// </summary>
    /// <param name="ninjadogSettings">The settings to be used for code generation.</param>
    /// <returns>A string representing the generated code, or null if not implemented.</returns>
    public virtual NinjadogContentFile? GenerateOne(NinjadogSettings ninjadogSettings)
    {
        // Default implementation: return null
        // Override this method in derived classes to implement specific logic
        // for generating code in a single file.
        return null;
    }

    /// <summary>
    /// Generates content for a single entity. This method can be overridden in derived classes
    /// to provide specific generation logic for each entity.
    /// </summary>
    /// <param name="entity">The entity for which to generate content.</param>
    /// <param name="rootNamespace">The root namespace to be used in content generation.</param>
    /// <returns>A string representing the generated content for the specified entity.</returns>
    public virtual NinjadogContentFile? GenerateOneByEntity(NinjadogEntityWithKey entity, string rootNamespace)
    {
        // Default implementation for generating content for a single entity.
        // This can be overridden in derived classes for specific entity generation logic.
        return null;
    }

    /// <summary>
    /// Generates code in multiple files based on the provided settings.
    /// This method iterates over entities and generates content for each.
    /// It can be overridden by derived classes to implement specific logic
    /// for one-to-many code generation.
    /// </summary>
    /// <param name="ninjadogSettings">The settings to be used for code generation.</param>
    /// <returns>An enumerable of strings, each representing generated code for an entity.</returns>
    public virtual IEnumerable<NinjadogContentFile> GenerateMany(NinjadogSettings ninjadogSettings)
    {
        var entities = ninjadogSettings.Entities.FromKeys();
        var rootNs = ninjadogSettings.Config.RootNamespace;

        foreach (var entity in entities)
        {
            var content = GenerateOneByEntity(entity, rootNs);
            if (content != null)
            {
                yield return content;
            }
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="NinjadogContentFile"/> with the specified file name and content.
    /// </summary>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="content">The content of the file to be created.</param>
    /// <param name="useDefaultLayout">Whether to use the default code layout.</param>
    /// <returns>A new instance of <see cref="NinjadogContentFile"/>.</returns>
    protected NinjadogContentFile CreateNinjadogContentFile(
        string fileName, string content, bool useDefaultLayout = true)
    {
        var contentWithLayout = useDefaultLayout
            ? TemplateUtilities.DefaultCodeLayout(content)
            : content;

        return new NinjadogContentFile(fileName, contentWithLayout, Category);
    }
}
