using System;
using UnityEngine;

namespace Roughlike2048.Event
{
    public class BaseVariable<TType> : BaseEvent<TType>, ISerializationCallbackReceiver
    {
        [SerializeField] TType initializeValue;
        [SerializeField] bool isSavable;
        [SerializeField] bool isRaiseEvent;
        [NonSerialized] TType runtimeValue;

        public TType Value
        {
            get => runtimeValue;
            set
            {
                runtimeValue = value;
                if (isRaiseEvent)
                {
                    Raise(value);
                }
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initializeValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}