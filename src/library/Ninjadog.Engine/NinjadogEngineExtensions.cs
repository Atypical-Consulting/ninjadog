// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Engine.Core.Abstractions;

namespace Ninjadog.Engine;

/// <summary>
/// Extension methods for the event handlers of the <see cref="NinjadogEngine"/> class.
/// </summary>
public static class NinjadogEngineExtensions
{
    /// <summary>
    /// Safely invokes the <see cref="EventHandler"/> event handler.
    /// </summary>
    /// <param name="eventHandler">The event handler to invoke.</param>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="args">The event arguments.</param>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    public static void SafeInvokeEvent<TEventArgs>(
        this EventHandler<TEventArgs>? eventHandler,
        INinjadogEngine? sender,
        TEventArgs args)
        where TEventArgs : System.EventArgs
    {
        SafeInvokeEvent(() => eventHandler?.Invoke(sender, args));
    }

    /// <summary>
    /// Safely invokes the <see cref="EventHandler"/> event handler.
    /// </summary>
    /// <param name="eventHandler">The event handler to invoke.</param>
    /// <param name="sender">The object that raised the event.</param>
    public static void SafeInvokeEvent(
        this EventHandler? eventHandler,
        INinjadogEngine? sender)
    {
        SafeInvokeEvent(() => eventHandler?.Invoke(sender, System.EventArgs.Empty));
    }

    private static void SafeInvokeEvent(Action? eventAction)
    {
        try
        {
            eventAction?.Invoke();
        }
        catch (Exception)
        {
            // Log the exception or handle it as necessary
        }
    }
}
