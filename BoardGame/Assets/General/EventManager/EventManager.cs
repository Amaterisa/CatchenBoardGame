using System.Collections.Generic;
using UnityEngine.Events;

namespace General.EventManager
{
    public static class EventManager
    {
        private static readonly Dictionary<int, UnityEvent> VoidEvents = new Dictionary<int, UnityEvent>();
        private static readonly Dictionary<int, object> ArgsEvents = new Dictionary<int, object>();

        #region Void Events

        public static void Register(int type, UnityAction action)
        {
            if (VoidEvents != null && !VoidEvents.ContainsKey(type))
                VoidEvents.Add(type, new UnityEvent());
            VoidEvents[type].AddListener(action);
        }

        public static void Unregister(int type, UnityAction action)
        {
            if (VoidEvents != null && VoidEvents.ContainsKey(type))
                VoidEvents[type].RemoveListener(action);
        }

        public static void Trigger(int type)
        {
            if (VoidEvents != null && VoidEvents.ContainsKey(type))
                VoidEvents[type].Invoke();
        }

        #endregion

        #region Args Events

        private class BaseArgEvent<T> : UnityEvent<T>
        {
        }

        private class BaseArgEvent<T, Z> : UnityEvent<T, Z>
        {
        }

        private class BaseArgEvent<T, Z, W> : UnityEvent<T, Z, W>
        {
        }

        public static void Register<T>(int type, UnityAction<T> action)
        {
            if (ArgsEvents != null && !ArgsEvents.ContainsKey(type))
            {
                ArgsEvents.Add(type, new BaseArgEvent<T>());
            }

            (ArgsEvents[type] as BaseArgEvent<T>).AddListener(action);
        }

        public static void Unregister<T>(int type, UnityAction<T> action)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type))
                (ArgsEvents[type] as BaseArgEvent<T>).RemoveListener(action);
        }

        public static void Trigger<T>(int type, T value)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type))
                (ArgsEvents[type] as BaseArgEvent<T>).Invoke(value);
        }

        public static void Register<T, Z>(int type, UnityAction<T, Z> action)
        {
            if (ArgsEvents != null && !ArgsEvents.ContainsKey(type))
            {
                ArgsEvents.Add(type, new BaseArgEvent<T, Z>());
            }

            (ArgsEvents[type] as BaseArgEvent<T, Z>).AddListener(action);
        }

        public static void Unregister<T, Z>(int type, UnityAction<T, Z> action)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type))
                (ArgsEvents[type] as BaseArgEvent<T, Z>).RemoveListener(action);
        }

        public static void Trigger<T, Z>(int type, T value, Z value2)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type))
                (ArgsEvents[type] as BaseArgEvent<T, Z>).Invoke(value, value2);
        }

        public static void Register<T, Z, W>(int type, UnityAction<T, Z, W> action)
        {
            if (ArgsEvents != null && !ArgsEvents.ContainsKey(type))
            {
                ArgsEvents.Add(type, new BaseArgEvent<T, Z, W>());
            }

            (ArgsEvents[type] as BaseArgEvent<T, Z, W>).AddListener(action);
        }

        public static void Unregister<T, Z, W>(int type, UnityAction<T, Z, W> action)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type))
                (ArgsEvents[type] as BaseArgEvent<T, Z, W>).RemoveListener(action);
        }

        public static void Trigger<T, Z, W>(int type, T value, Z value2, W value3)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type))
                (ArgsEvents[type] as BaseArgEvent<T, Z, W>).Invoke(value, value2, value3);
        }

        #endregion
    }
}