// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates;

/// <summary>
/// Represents a collection of Ninjadog templates.
/// This class provides functionalities to manage a list of template instances.
/// </summary>
public class NinjadogTemplateFiles : List<NinjadogTemplate>
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
            // The category can be used for organizational purposes
            this.Add(template);
        }
    }
}
