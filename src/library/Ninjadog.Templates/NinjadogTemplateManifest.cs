// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates;

/// <summary>
/// Represents an abstract base for a Ninjadog template manifest.
/// This class serves as a blueprint for defining the metadata and associated files for a specific template.
/// </summary>
public abstract class NinjadogTemplateManifest
{
    /// <summary>
    /// Gets the name of the template manifest.
    /// </summary>
    public virtual string Name { get; init; }

    /// <summary>
    /// Gets the description of the template manifest.
    /// </summary>
    public virtual string Description { get; init; }

    /// <summary>
    /// Gets the version of the template manifest.
    /// </summary>
    public virtual string Version { get; init; }

    /// <summary>
    /// Gets the author of the template manifest.
    /// </summary>
    public virtual string Author { get; init; }

    /// <summary>
    /// Gets the license under which the template manifest is distributed.
    /// </summary>
    public virtual string License { get; init; }

    /// <summary>
    /// Gets the collection of template files associated with this manifest.
    /// </summary>
    public virtual NinjadogTemplateFiles TemplateFiles { get; init; }
}
