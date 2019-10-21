using System;

namespace Utility
{
    internal static class Enum<T> where T : Enum
    {
        public static void ForEach(Action<T> action)
        {
            var type = typeof(T);
            var values = type.GetEnumValues();

            foreach (var value in values)
            {
                action.Invoke((T) value);
            }
        }
    }
}