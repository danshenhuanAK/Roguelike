using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action StartNewGameEvent;
    public static void CallStartNewGameEvent()
    {
        StartNewGameEvent?.Invoke();
    }
}
