// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Internals;

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
    public static void SafeInvoke<TEventArgs>(
        this EventHandler<TEventArgs>? eventHandler,
        object? sender,
        TEventArgs args)
    {
        SafeInvokeEvent(() => eventHandler?.Invoke(sender, args));
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
