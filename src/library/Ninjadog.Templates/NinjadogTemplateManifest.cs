// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates;

public abstract class NinjadogTemplateManifest
{
    public virtual string Name { get; init; }
    public virtual string Description { get; init; }
    public virtual string Version { get; init; }
    public virtual string Author { get; init; }
    public virtual string License { get; init; }
    public virtual NinjadogTemplateFiles TemplateFiles { get; init; }
}
