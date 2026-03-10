// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using System.Text.RegularExpressions;

namespace Ninjadog.Helpers;

internal sealed class InflectorRule(string pattern, string replacement)
{
    private readonly Regex _regex = new(pattern, RegexOptions.IgnoreCase);

    public string? Apply(string word)
    {
        return _regex.IsMatch(word)
            ? _regex.Replace(word, replacement)
            : null;
    }
}
