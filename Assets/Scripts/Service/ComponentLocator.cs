using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Service
{
    public static class ComponentLocator
    {
        private static readonly Dictionary<Type, Object> Components;

        static ComponentLocator()
        {
            Components = new Dictionary<Type, Object>();
        }

        public static void Register<T>(T component) where T : Object
        {
            var type = component.GetType();

            if (Components.ContainsKey(type))
            {
                if (Components[type] == null)
                {
                    Components[type] = component;
                    return;
                }

                Object.Destroy(component);
                return;
            }

            Components.Add(type, component);
        }

        public static T Resolve<T>() where T : Object
        {
            var type = typeof(T);
            if (Components.TryGetValue(type, out var component))
            {
                return component as T;
            }

            return Object.FindObjectOfType<T>();
        }
    }
}