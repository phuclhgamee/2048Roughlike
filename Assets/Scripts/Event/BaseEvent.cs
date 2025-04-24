using System.Collections.Generic;
using UnityEngine;

namespace Roughlike2048.Event
{
    public class BaseEvent : ScriptableObject, IEvent
    {
        private List<BaseEventListener> listeners = new List<BaseEventListener>();
    
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
    
        public void RegisterListener(BaseEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }
    
        public void UnregisterListener(BaseEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
    
    public class BaseEvent<T> : ScriptableObject, IEvent
    {
        private List<BaseEventListener<T>> listeners = new List<BaseEventListener<T>>();

        public void Raise(T data)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(data);
            }
        }

        public void RegisterListener(BaseEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(BaseEventListener<T> listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}