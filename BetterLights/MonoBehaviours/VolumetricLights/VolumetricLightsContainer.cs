using System.Collections.Generic;
using UnityEngine;

namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public class VolumetricLightsContainer : MonoBehaviour
    {
        private static VolumetricLightsContainer instance;
        private static VolumetricLightsContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(VolumetricLightsContainer).Name).AddComponent<VolumetricLightsContainer>();
                }
                return instance;
            }
        }

        private Dictionary<int, IVolumetricLightsController> dict = new();
        public static Dictionary<int, IVolumetricLightsController> Dict => Instance.dict;

        private float lastUpdate = 0f;
        public float UpdateInterval { get; set; } = 60f;

        protected void Update()
        {
            if (lastUpdate == 0 || lastUpdate + UpdateInterval < Time.time)
            {
                foreach (var item in dict)
                {
                    if (item.Value == null)
                    {
                        dict.Remove(item.Key);
                    }
                }

                lastUpdate = Time.time;
            }
        }
    }
}
