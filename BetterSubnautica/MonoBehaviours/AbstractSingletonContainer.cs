using BetterSubnautica.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours
{
    public abstract class AbstractSingletonContainer<Type, Key, Value> : MonoBehaviour where Type : AbstractSingletonContainer<Type, Key, Value>
    {
        private static Type instance;
        public static Type Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(Type).Name).AddComponent<Type>();
                }
                return instance;
            }
        }

        public virtual IDictionary<Key, Value> Dict { get; } = new Dictionary<Key, Value>();

        private float lastUpdate = 0f;
        private float updateInterval = 60f;

        protected virtual void Update()
        {
            //DebuggerUtility.ShowMessage($"Dict -> {Dict.Count}", $"({GetInstanceID()}) {GetType().Name}.Update");

            if (lastUpdate == 0 || lastUpdate + updateInterval < Time.time)
            {
                foreach (var item in Dict)
                {
                    if (item.Value == null)
                    {
                        Dict.Remove(item.Key);
                    }
                }

                lastUpdate = Time.time;
            }
        }
    }
}
