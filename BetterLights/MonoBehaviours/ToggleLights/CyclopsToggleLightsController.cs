#if SUBNAUTICA
using BetterSubnautica.Extensions;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class CyclopsToggleLightsController : AbstractToggleLightsController<SubRoot>
    {
        private CyclopsLightingPanel lightingPanel = null;
        private CyclopsExternalCams externalCams = null;
        private PowerRelay powerRelay = null;

        public override bool LightsActive => lightsActive = externalCams.GetUsingCameras() || lightingPanel.lightingOn || lightingPanel.floodlightsOn;

        protected override bool KeyDown => false;

        public override float EnergyConsumption
        {
            get
            {
                var energyPerSecond = 0f;
                if (lightingPanel.lightingOn)
                {
                    if (component.lightingState == 0 && Player.main.currentSub == component)
                    {
                        energyPerSecond += Core.CyclopsSettings.InternalLightsConsumption;
                    }
                    else
                    {
                        energyPerSecond += Core.CyclopsSettings.InternalLightsConsumption / 2;
                    }
                }
                if (externalCams.GetUsingCameras())
                {
                    switch (externalCams.GetLightState())
                    {
                        case 1:
                            energyPerSecond += Core.CyclopsSettings.CameraLightsConsumption;
                            break;
                        case 2:
                            energyPerSecond += Core.CyclopsSettings.CameraLightsConsumption / 2;
                            break;
                    }
                }
                else
                {
                    if (lightingPanel.floodlightsOn)
                    {
                        energyPerSecond += Core.CyclopsSettings.ExternalLightsConsumption;
                    }
                }
                return energyPerSecond;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                lightingPanel = component.gameObject.GetComponentInChildren<CyclopsLightingPanel>();
                externalCams = component.gameObject.GetComponentInChildren<CyclopsExternalCams>();
                powerRelay = component.GetPowerRelay();

                if (!component.isCyclops || lightingPanel == null || externalCams == null || powerRelay == null)
                {
                    Destroy(this);
                }
            }
        }

        protected override void Start()
        {
            SetLightsActive(lightsActive, true);
        }

        protected override bool CanToggleLightsActive()
        {
            return false;
        }

        public override void SetLightsActive(bool active, bool force = false)
        {
            if (!IsPowered())
            {
                active = false;
            }

            lightingPanel.lightingOn = active;
            lightingPanel.floodlightsOn = active;
            component.ForceLightingState(active);
            lightingPanel.SetExternalLighting(active);
            lightingPanel.UpdateLightingButtons();
        }

        protected override bool IsPowered()
        {
            return powerRelay.GetPower() > 0f;
        }

        protected override void ConsumeEnergy(float amount)
        {
            powerRelay.ConsumeEnergy(amount, out var _);
        }
    }
}
#endif
