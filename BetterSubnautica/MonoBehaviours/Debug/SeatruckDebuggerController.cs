#if BELOWZERO
using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class SeatruckDebuggerController : AbstractDebuggerController<SeaTruckSegment>
    {
        public override bool ShowDebugInfo => Core.Settings.SeatruckInfo;

        private SeaTruckLights seaTruckLights = null;
        private SeaTruckLights SeaTruckLights
        {
            get
            {
                if (seaTruckLights == null && Component != null)
                {
                    seaTruckLights = Component.gameObject.GetComponent<SeaTruckLights>();
                }
                return seaTruckLights;
            }
        }

        private GameObject lightsParent;
        private GameObject LightsParent
        {
            get
            {
                if (lightsParent == null && SeaTruckLights != null)
                {
                    lightsParent = SeaTruckLights.floodLight;
                }
                return lightsParent;
            }
        }

        private PowerRelay powerRelay = null;
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

        protected override LightsType LightsType => LightsActive ? LightsType.External : LightsType.None;

        protected override bool LightsActive
        {
            get
            {
                var lightsActive = false;
                if (LightsParent != null)
                {
                    lightsActive = LightsParent.activeInHierarchy;
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
