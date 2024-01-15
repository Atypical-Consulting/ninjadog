using Ninjadog.Engine.EventArgs;

namespace Ninjadog.Engine.Abstractions;

/// <summary>
/// Defines the interface for handling errors that occur during the execution of the Ninjadog Engine.
/// </summary>
public interface INinjadogEngineErrorHandling
{
    /// <summary>
    /// Occurs when an error is encountered during the operation of the Ninjadog Engine.
    /// This event allows for error logging, analysis, or handling specific exceptions that occur during processing.
    /// </summary>
    event EventHandler<NinjadogErrorEventArgs>? OnErrorOccurred;
}