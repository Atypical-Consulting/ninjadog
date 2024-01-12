// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Ninjadog.Settings;

namespace Ninjadog.Templates;

public abstract class NinjadogTemplate
{
    public virtual string? GenerateOneToOne(NinjadogSettings ninjadogSettings)
    {
        // This method is intended to be overridden by derived classes
        // that generate code in a single file.

        // Called by the engine when the template is configured to generate

        return null;
    }

    public virtual IEnumerable<string> GenerateOneToMany(NinjadogSettings ninjadogSettings)
    {
        // This method is intended to be overridden by derived classes
        // that generate code in multiple files.

        // Called by the engine when the template is configured to generate

        return [];
    }
}
