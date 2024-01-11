// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Core.Helpers;
using Ninjadog.Settings;

namespace Ninjadog.Templates;

public abstract class NinjadogTemplate
{
    protected abstract string GetClassName(
        StringTokens stringTokens);

    protected abstract string GetSubNamespace(
        StringTokens stringTokens);

    public virtual string? GenerateSingleFile(NinjadogSettings ninjadogSettings)
    {
        // This method is intended to be overridden by derived classes
        // that generate code in a single file.

        // Called by the engine when the template is configured to generate

        return null;
    }

    public virtual IEnumerable<string?> GenerateMultipleFiles(NinjadogSettings ninjadogSettings)
    {
        // This method is intended to be overridden by derived classes
        // that generate code in multiple files.

        // Called by the engine when the template is configured to generate

        return [];
    }
}
