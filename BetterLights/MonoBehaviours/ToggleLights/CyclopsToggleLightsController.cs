#if SUBNAUTICA
using BetterSubnautica.Extensions;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class CyclopsToggleLightsController : AbstractToggleLightsController<SubRoot>
    {
        public override bool MandatoryLightsParent => false;
        public override bool MandatoryToggleLights => false;

        private CyclopsLightingPanel lightingPanel = null;
        private CyclopsExternalCams externalCams = null;

        public override bool KeyDown => false;

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

        public override bool LightsActive => lightsActive = externalCams.GetUsingCameras() || lightingPanel.lightingOn || lightingPanel.floodlightsOn;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                lightingPanel = component.gameObject.GetComponentInChildren<CyclopsLightingPanel>();
                externalCams = component.gameObject.GetComponentInChildren<CyclopsExternalCams>();

                if (!component.isCyclops || lightingPanel == null || externalCams == null)
                {
                    Destroy(this);
                }
            }
        }

        public override bool CanToggleLightsActive()
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
            lightingPanel.SetExternalLighting(active);
            lightingPanel.UpdateLightingButtons();
            component.ForceLightingState(active);
        }
    }
}
#endif
