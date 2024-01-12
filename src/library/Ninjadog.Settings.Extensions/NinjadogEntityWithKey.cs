// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Settings.Extensions;

public sealed record NinjadogEntityWithKey(
    string Key, NinjadogEntityProperties Properties)
    : NinjadogEntity(Properties)
{
    public StringTokens StringTokens => new(Key);

    public string GenerateMemberProperties()
    {
        return Properties
            .FromKeys()
            .Select(p => p.GenerateMemberProperty())
            .Aggregate((x, y) => $"{x}\n{y}");
    }
}
