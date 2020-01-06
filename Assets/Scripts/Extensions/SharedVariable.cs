using UnityEngine;

namespace Extensions
{
    public class SharedVariable<T> : ScriptableObject
    {
        [SerializeField]
        private T serializedValue;

        private T Value { get; set; }

        private void Awake()
        {
            Value = serializedValue;
        }

        public void SetValue(T newValue)
        {
            Value = newValue;
        }

        public T GetValue()
        {
            return Value;
        }
    }
}