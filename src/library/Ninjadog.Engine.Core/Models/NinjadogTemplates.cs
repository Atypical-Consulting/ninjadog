// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Represents a collection of Ninjadog templates.
/// This class provides functionalities to manage a list of template instances.
/// </summary>
public class NinjadogTemplates : List<NinjadogTemplate>
{
    /// <summary>
    /// Adds multiple templates to the collection, potentially under a specified category.
    /// </summary>
    /// <param name="category">The category under which to group the templates.</param>
    /// <param name="templates">The templates to be added to the collection.</param>
    public void AddTemplates(string category, params NinjadogTemplate[] templates)
    {
        foreach (var template in templates)
        {
            // Set the category for each template
            template.Category = category;

            // Add the template to the collection
            Add(template);
        }
    }
}

public interface INinjadogTemplateFactory
{
    /// <summary>
    /// Creates a new instance of the specified template.
    /// </summary>
    /// <param name="templateName">The name of the template to be created.</param>
    /// <returns>A new instance of the specified template.</returns>
    NinjadogTemplate CreateTemplate(string templateName);
}
