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
    public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs>? eventHandler, object? sender, TEventArgs args)
    {

        SafeInvokeEvent(() => eventHandler?.Invoke(sender, args));
    }

    private static void SafeInvokeEvent(Action? eventAction)
    {
        try
        {
            eventAction?.Invoke();
        }
        catch (Exception _)
        {
            // Log the exception or handle it as necessary
        }
    }
}
