using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class AnimationEventDispatcher : MonoBehaviour
{
    public UnityEvent onAnimationComplete = new UnityEvent();

    private static List<Action> onAnimationCompleteListeners = new List<Action>();

    public static event Action OnAnimationComplete
    {
        add
        {
            onAnimationCompleteListeners.Add(value);
        }
        remove
        {
            onAnimationCompleteListeners.Remove(value);
        }
    }

    public void TriggerOnAnimationComplete()
    {
        foreach (var listener in onAnimationCompleteListeners)
        {
            listener?.Invoke();
        }
        onAnimationComplete.Invoke();
    }
}


