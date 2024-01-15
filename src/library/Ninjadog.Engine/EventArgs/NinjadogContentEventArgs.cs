// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Templates;

namespace Ninjadog.Engine.EventArgs;

/// <summary>
/// Provides data for events where a NinjadogContentFile is involved, such as when content is processed.
/// </summary>
public class NinjadogContentEventArgs
    : System.EventArgs
{
    /// <summary>
    /// Gets the content file that is being processed.
    /// </summary>
    public required NinjadogContentFile ContentFile { get; init; }
}
