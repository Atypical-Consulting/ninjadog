// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates;

/// <summary>
/// Provides metadata generation for template headers, including developer information, generation date, and version.
/// This class encapsulates the constants and methods necessary to generate standardized metadata
/// for use in template generation and documentation.
/// </summary>
public class TemplateHeaderMetadata
{
    /// <summary>
    /// The name of the developer.
    /// </summary>
    public const string DeveloperName = "Philippe Matray";

    /// <summary>
    /// The name of the company.
    /// </summary>
    public const string CompanyName = "Atypical Consulting SRL";

    /// <summary>
    /// Gets the current generation date in a standardized format.
    /// </summary>
    public static string GenerationDate
        => DateTime.Now.ToString("R");

    /// <summary>
    /// Gets the version of the assembly where this class is defined.
    /// </summary>
    public static string Version
        => typeof(TemplateHeaderMetadata).Assembly.GetName().Version?.ToString() ?? "0.0.0";
}
