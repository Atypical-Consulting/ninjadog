using Ninjadog.Engine.EventArgs;

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for handling content processing events in the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineContentProcessing
{
    /// <summary>
    /// Occurs before a content file is processed by the Ninjadog Engine.
    /// This event can be used to inspect or modify the content file before it undergoes further processing.
    /// </summary>
    event EventHandler<NinjadogContentEventArgs>? OnBeforeContentProcessed;

    /// <summary>
    /// Occurs after a content file has been processed by the Ninjadog Engine.
    /// This event allows for actions to be taken after a content file has been fully processed, such as additional formatting or logging.
    /// </summary>
    event EventHandler<NinjadogContentEventArgs>? OnAfterContentProcessed;
}