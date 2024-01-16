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
    /// Wraps the provided code in a default layout, including headers and nullability annotations.
    /// This method adds common elements like file headers and nullability enable/disable statements
    /// to the provided code, creating a standardized format for generated source files.
    /// </summary>
    /// <param name="code">The code snippet to be wrapped in the default layout.</param>
    /// <returns>The code wrapped in the default layout with necessary headers and nullability annotations.</returns>
    public static string DefaultCodeLayout(string code)
    {
        return SourceGenerationHelper.Header +
               SourceGenerationHelper.NullableEnable +
               "\n" +
               code +
               "\n" +
               SourceGenerationHelper.NullableDisable;
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
}
