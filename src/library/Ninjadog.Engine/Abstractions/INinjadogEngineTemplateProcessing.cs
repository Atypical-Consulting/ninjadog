using Ninjadog.Engine.EventArgs;

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for handling template processing events in the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineTemplateProcessing
{
    /// <summary>
    /// Occurs before a template is processed by the Ninjadog Engine.
    /// This event provides an opportunity to perform actions or modifications before each template processing begins.
    /// </summary>
    event EventHandler<NinjadogTemplateEventArgs>? OnBeforeTemplateProcessed;

    /// <summary>
    /// Occurs after a template has been processed by the Ninjadog Engine.
    /// This event is useful for post-processing actions or for gathering information after a template has been processed.
    /// </summary>
    event EventHandler<NinjadogTemplateEventArgs>? OnAfterTemplateProcessed;
}