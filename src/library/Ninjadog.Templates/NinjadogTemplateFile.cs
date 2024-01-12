// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

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
