using System.Linq;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public abstract class AbstractVolumetricLightsController<T> : MonoBehaviour, IVolumetricLightsController where T : Component
    {
        protected T component;

        protected VFXVolumetricLight[] volumetricLights;
        public virtual VFXVolumetricLight[] VolumetricLights
        {
            get
            {
                if (volumetricLights == null)
                {
                    volumetricLights = [];
                }
                return volumetricLights;
            }
        }

        protected float intensityOffset;
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

        public void OnDestroy()
        {
            //DebuggerUtility.ShowMessage($"Component: {component != null} | VolumetricLights: {VolumetricLights.Length}", $"({GetInstanceID()}) {GetType().Name}.Destroy");

            foreach (var item in VolumetricLights)
            {
                VolumetricLightsContainer.Instance.Dict.Remove(item.GetInstanceID());
            }
        }

        protected virtual void Awake()
        {
            component = gameObject.GetComponent<T>();

            if (component == null)
            {
                Destroy(this);
            }
        }

        protected virtual void Start()
        {
            foreach (var volumetricLight in VolumetricLights)
            {
                VolumetricLightsContainer.Instance.Dict[volumetricLight.GetInstanceID()] = this;
            }
        }

        protected void LateUpdate()
        {
            UpdateSettings();
        }

        public void UpdateMaterial(VFXVolumetricLight volumetricLight, bool forceUpdate)
        {
            if (VolumetricLights.Contains(volumetricLight))
            {
                volumetricLight.UpdateMaterial(IntensityOffset, forceUpdate);
            }
        }

        protected abstract void UpdateSettings();
    }
}
