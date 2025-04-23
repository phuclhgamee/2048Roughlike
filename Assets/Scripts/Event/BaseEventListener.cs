using UnityEngine;
using UnityEngine.Events;

namespace Roughlike2048.Event
{
    public class BaseEventListener : MonoBehaviour
    {
        public BaseEvent BaseEvent;
        public UnityEvent response;

        private void OnEnable()
        {
            BaseEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            BaseEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
    
    public class BaseEventListener<T> : MonoBehaviour
    {
        public BaseEvent<T> BaseEvent;
        public UnityEvent<T> response;

        private void OnEnable()
        {
            BaseEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            BaseEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T data)
        {
            response.Invoke(data);
        }
    }
}