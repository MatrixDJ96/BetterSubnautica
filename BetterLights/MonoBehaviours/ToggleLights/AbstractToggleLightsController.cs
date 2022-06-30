using BetterSubnautica.Extensions;
using UnityEngine;
using GameUtils = Utils;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public abstract class AbstractToggleLightsController<T> : MonoBehaviour, IToggleLightsController where T : Component
    {
        protected T component = null;

        protected GameObject lightsParent = null;

        protected bool lightsActive = false;
        public virtual bool LightsActive => lightsActive;

        protected FMOD_StudioEventEmitter lightsOnSound = null;
        protected FMOD_StudioEventEmitter lightsOffSound = null;
        protected FMODAsset onSound = null;
        protected FMODAsset offSound = null;

        protected abstract bool KeyDown { get; }

        public abstract float EnergyConsumption { get; }

        protected virtual void Awake()
        {
            component = gameObject.GetComponent<T>();

            if (component == null)
            {
                Destroy(this);
                return;
            }

            lightsParent = component.GetLightsParent();
        }

        protected virtual void Start()
        {
            SetLightsActive(LightsActive, true);
        }

        protected virtual void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                UpdateLightsEnergy();

                if (CanToggleLightsActive())
                {
                    ToggleLightsActive();
                }

                FixLightsParent();
            }
        }

        private void FixLightsParent()
        {
            if (lightsParent != null)
            {
                lightsParent.SetActive(lightsActive);
            }
        }

        protected virtual void UpdateLightsEnergy()
        {
            if (!IsPowered())
            {
                SetLightsActive(false);
            }
            else if (LightsActive && EnergyConsumption > 0f)
            {
                ConsumeEnergy(DayNightCycle.main.deltaTime * EnergyConsumption);
            }
        }

        protected virtual bool CanToggleLightsActive()
        {
            return KeyDown && !Player.main.GetPDA().isInUse;
        }

        public virtual void SetLightsActive(bool active, bool force = false)
        {
            if (!IsPowered())
            {
                active = false;
            }

            if (LightsActive != active || force)
            {
                if (LightsActive != active)
                {
                    if (active)
                    {
                        if ((bool)lightsOnSound)
                        {
                            GameUtils.PlayEnvSound(lightsOnSound, lightsOnSound.gameObject.transform.position);
                        }
                        if ((bool)onSound)
                        {
                            GameUtils.PlayFMODAsset(onSound, transform);
                        }
                    }
                    else
                    {
                        if ((bool)lightsOffSound)
                        {
                            GameUtils.PlayEnvSound(lightsOffSound, lightsOffSound.gameObject.transform.position);
                        }
                        if ((bool)offSound)
                        {
                            GameUtils.PlayFMODAsset(offSound, transform);
                        }
                    }
                }

                lightsActive = active;
            }

            FixLightsParent();
        }

        public void ToggleLightsActive()
        {
            SetLightsActive(!LightsActive);
        }

        protected abstract bool IsPowered();

        protected abstract void ConsumeEnergy(float amount);
    }
}
