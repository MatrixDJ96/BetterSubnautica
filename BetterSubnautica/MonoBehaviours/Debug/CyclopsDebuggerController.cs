#if SUBNAUTICA
using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class CyclopsDebuggerController : AbstractDebuggerController<SubRoot>
    {
        public override bool ShowDebugInfo => Core.Settings.CyclopsInfo;

        private CyclopsLightingPanel lightingPanel;
        private CyclopsLightingPanel LightingPanel
        {
            get
            {
                if (lightingPanel == null && Component != null)
                {
                    lightingPanel = Component.gameObject.GetComponentInChildren<CyclopsLightingPanel>();
                }
                return lightingPanel;
            }
        }

        private CyclopsExternalCams externalCams;
        private CyclopsExternalCams ExternalCams
        {
            get
            {
                if (externalCams == null && Component != null)
                {
                    externalCams = Component.gameObject.GetComponentInChildren<CyclopsExternalCams>();
                }
                return externalCams;
            }
        }

        private PowerRelay powerRelay;
        private PowerRelay PowerRelay
        {
            get
            {
                if (powerRelay == null && Component != null)
                {
                    powerRelay = Component.GetPowerRelay();
                }
                return powerRelay;
            }
        }

        protected override LightsType LightsType
        {
            get
            {
                var lightsType = LightsType.None;
                if (LightingPanel != null && LightingPanel.lightingOn)
                {
                    lightsType |= LightsType.Internal;
                }
                if (ExternalCams != null && ExternalCams.GetActive())
                {
                    if (ExternalCams.GetLightState() > 0)
                    {
                        lightsType |= LightsType.Camera;
                    }
                }
                else if (LightingPanel != null && LightingPanel.floodlightsOn)
                {
                    lightsType |= LightsType.External;
                }
                return lightsType;
            }
        }

        protected override bool LightsActive
        {
            get
            {
                var lightsActive = false;
                if (LightingPanel != null)
                {
                    lightsActive |= LightingPanel.lightingOn || LightingPanel.floodlightsOn;
                }
                if (ExternalCams != null)
                {
                    lightsActive |= ExternalCams.GetActive() && ExternalCams.GetLightState() > 0;
                }
                return lightsActive;
            }
        }

        protected override float Capacity
        {
            get
            {
                var capacity = 0f;
                if (PowerRelay != null)
                {
                    capacity = PowerRelay.GetMaxPower();
                }
                return capacity;
            }
        }

        protected override float Charge
        {
            get
            {
                var charge = 0f;
                if (PowerRelay != null)
                {
                    charge = PowerRelay.GetPower();
                }
                return charge;
            }
        }
    }
}
#endif
