// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Settings;

public record NinjadogSettings
{
    public virtual NinjadogConfiguration Config { get; init; }
    public virtual NinjadogEntities Entities { get; init; }
}
