// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Templates;

namespace Ninjadog.Engine.EventArgs;

/// <summary>
/// Provides data for the RunCompleted event of NinjadogEngine.
/// </summary>
/// <param name="runTime">The duration of the run operation.</param>
public class NinjadogEngineRunEventArgs(TimeSpan runTime)
    : System.EventArgs
{
    /// <summary>
    /// Gets the duration of the run operation.
    /// </summary>
    public TimeSpan RunTime { get; } = runTime;
}

/// <summary>
/// Provides data for events where a NinjadogContentFile is involved, such as when content is processed.
/// </summary>
/// <param name="contentFile">The content file that is being processed.</param>
public class NinjadogContentEventArgs(NinjadogContentFile contentFile)
    : System.EventArgs
{
    /// <summary>
    /// Gets the content file that is being processed.
    /// </summary>
    public NinjadogContentFile ContentFile { get; } = contentFile;
}

/// <summary>
/// Provides data for events that occur when an exception is encountered in the NinjadogEngine.
/// </summary>
/// <param name="exception">The exception that was thrown.</param>
public class NinjadogErrorEventArgs(Exception exception)
    : System.EventArgs
{
    /// <summary>
    /// Gets the exception that was thrown.
    /// </summary>
    public Exception Exception { get; } = exception;
}

/// <summary>
/// Provides data for events related to the processing of a NinjadogTemplate, such as before or after a template is processed.
/// </summary>
/// <param name="template">The template that is being processed.</param>
public class NinjadogTemplateEventArgs(NinjadogTemplate template)
    : System.EventArgs
{
    /// <summary>
    /// Gets the template that is being processed.
    /// </summary>
    public NinjadogTemplate Template { get; } = template;
}
