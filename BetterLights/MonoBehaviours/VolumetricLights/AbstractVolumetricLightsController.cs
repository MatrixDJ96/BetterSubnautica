using BetterSubnautica.Extensions;
using System;
using System.Linq;
using UnityEngine;

namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public abstract class AbstractVolumetricLightsController<T> : MonoBehaviour, IVolumetricLightsController where T : Component
    {
        protected T component = null;

        protected VFXVolumetricLight[] volumetricLights = Array.Empty<VFXVolumetricLight>();
        public virtual VFXVolumetricLight[] VolumetricLights => volumetricLights;

        protected float intensityOffset = 0f;
        public float IntensityOffset
        {
            get => intensityOffset;
            set
            {
                if (intensityOffset != value)
                {
                    intensityOffset = value;
                }
            }
        }

        public float UpdateInterval { get; set; } = 60f;

        public void OnDestroy()
        {
            foreach (var item in volumetricLights)
            {
                VolumetricLightsContainer.Dict.Remove(item.GetInstanceID());
            }
        }

        protected virtual void Awake()
        {
            component = gameObject.GetComponent<T>();

            if (component == null)
            {
                Destroy(this);
                return;
            }
        }

        protected virtual void Start()
        {
            foreach (var volumetricLight in volumetricLights)
            {
                VolumetricLightsContainer.Dict[volumetricLight.GetInstanceID()] = this;
            }
        }

        protected void LateUpdate()
        {
            UpdateSettings();
        }

        public void UpdateMaterial(VFXVolumetricLight volumetricLight, bool forceUpdate)
        {
            if (volumetricLights.Contains(volumetricLight))
            {
                volumetricLight.UpdateMaterial(IntensityOffset, forceUpdate);
            }
        }

        protected abstract void UpdateSettings();
    }
}
