// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Provides utility methods for common template generation tasks.
/// This static class includes functions for formatting and structuring code templates,
/// ensuring a consistent layout and style in generated code.
/// </summary>
public static class TemplateUtilities
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
    /// Wraps the provided code in a default layout, including headers and nullability annotations.
    /// This method adds common elements like file headers and nullability enable/disable statements
    /// to the provided code, creating a standardized format for generated source files.
    /// </summary>
    /// <param name="code">The code snippet to be wrapped in the default layout.</param>
    /// <param name="nullable">Whether to enable nullable reference types in the generated code.</param>
    /// <returns>The code wrapped in the default layout with necessary headers and nullability annotations.</returns>
    public static string DefaultCodeLayout(string code, bool nullable = false)
    {
        return Header +
               (nullable ? NullableEnable : string.Empty) +
               "\n" +
               code +
               "\n" +
               (nullable ? NullableDisable : string.Empty);
    }

    /// <summary>
    /// Generates a file-scoped namespace declaration based on the provided namespace string.
    /// This method creates a concise namespace declaration that applies to the entire source file,
    /// allowing for a more streamlined and modern code layout.
    /// </summary>
    /// <param name="ns">The namespace to be used in the declaration. If null, no declaration is generated.</param>
    /// <returns>A file-scoped namespace declaration, or null if the input namespace is null.</returns>
    public static string? WriteFileScopedNamespace(string? ns)
    {
        return ns is not null
            ? $"namespace {ns};"
            : null;
    }

    /// <summary>
    /// Gets a standard header comment for auto-generated source files.
    /// The header includes metadata such as the company and developer name, generation date, and version.
    /// It indicates that the file is auto-generated and warns against manual modifications.
    /// </summary>
    public static string Header =>
        $"""
         //------------------------------------------------------------------------------
         // This code was powered by the Ninjadog Engine
         //
         // Developed by: {CompanyName} - {DeveloperName}
         // Generated on: {GenerationDate:yyyy-MM-dd HH:mm:ss}
         // Version     : {Version}
         //
         // This file is part of a custom software solution crafted to meet your
         // specific needs. For optimal performance and compatibility, modifications
         // should be coordinated with Ninjadog support services.
         //------------------------------------------------------------------------------
         """;


    /// <summary>
    /// Gets a directive to enable nullable reference types in the generated code.
    /// This directive helps in ensuring that the code conforms to C# nullable reference types feature.
    /// </summary>
    public static string NullableEnable =>
        """

        #nullable enable
        """;

    /// <summary>
    /// Gets a directive to disable nullable reference types in the generated code.
    /// This can be used to revert to non-nullable reference types behavior in specific parts of the generated code.
    /// </summary>
    public static string NullableDisable =>
        """

        #nullable disable
        """;

    /// <summary>
    /// Gets the current generation date in a standardized format.
    /// </summary>
    public static string GenerationDate
        => DateTime.Now.ToString("R");

    /// <summary>
    /// Gets the version of the assembly where this class is defined.
    /// </summary>
    public static string Version
        => typeof(TemplateUtilities).Assembly.GetName().Version?.ToString() ?? "0.0.0";
}
