// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Models;

namespace Ninjadog.Engine.Core.EventArgs;

/// <summary>
/// Provides data for events related to the processing of a NinjadogTemplate, such as before or after a template is processed.
/// </summary>
public class NinjadogTemplateEventArgs
    : System.EventArgs
{
    /// <summary>
    /// Gets the template that is being processed.
    /// </summary>
    public required NinjadogTemplate Template { get; init; }
}
