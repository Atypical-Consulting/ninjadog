// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.
namespace Ninjadog.Helpers;

/// <summary>
/// Serves as the base record for handling naming conventions.
/// This record provides a common structure for generating various naming formats based on a given Pascal case string.
/// </summary>
/// <param name="Pascal">The Pascal case string from which other naming formats are derived.</param>
public abstract record NamingConventionBase(string Pascal)
{
    /// <summary>
    /// Gets the camel case version of the Pascal case string.
    /// </summary>
    public virtual string Camel
        => Pascal.Camelize();

    /// <summary>
    /// Gets the dashed (kebab case) version of the Pascal case string.
    /// </summary>
    public virtual string Dashed
        => Pascal.Underscore().Dasherize();

    /// <summary>
    /// Gets the humanized (lowercase with spaces) version of the Pascal case string.
    /// </summary>
    public virtual string Humanized
        => Pascal.Underscore().Humanize().ToLowerInvariant();

    /// <summary>
    /// Gets the pluralized camel case version of the Pascal case string.
    /// </summary>
    public virtual string CamelPlural
        => Camel.Pluralize();

    /// <summary>
    /// Gets the pluralized Pascal case version of the original string.
    /// </summary>
    public virtual string PascalPlural
        => Pascal.Pluralize();

    /// <summary>
    /// Gets the pluralized dashed (kebab case) version of the Pascal case string.
    /// </summary>
    public virtual string DashedPlural
        => PascalPlural.Underscore().Dasherize();

    /// <summary>
    /// Gets the pluralized humanized (lowercase with spaces) version of the Pascal case string.
    /// </summary>
    public virtual string HumanizedPlural
        => PascalPlural.Underscore().Humanize().ToLowerInvariant();
}
