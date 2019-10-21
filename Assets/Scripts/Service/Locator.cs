using System;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public static class Locator
    {
        private static readonly Dictionary<Type, object> Components;

        static Locator()
        {
            Components = new Dictionary<Type, object>();
        }
        
        public static void Register<T>(T component)
        {
            var type = typeof(T);
            
            Debug.Log($"Register<{type}>([{component.GetType()}] - {component.ToString()})");

            if (Components.ContainsKey(type))
            {
                Components[type] = component;
                return;
            }

            Components.Add(type, component);
        }

        public static T Resolve<T>()
        {
            var type = typeof(T);
            if (Components.TryGetValue(type, out var component))
            {
                return (T) component;
            }

            return default;
        }
    }
}