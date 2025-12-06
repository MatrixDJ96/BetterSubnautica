#if BELOWZERO
using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class SeatruckDebuggerController : AbstractDebuggerController<SeaTruckSegment>
    {
        public override bool ShowDebugInfo => Core.Settings.SeatruckInfo;

        private GameObject lightsParent;
        private GameObject LightsParent
        {
            get
            {
                if (lightsParent == null && Component != null)
                {
                    lightsParent = Component.GetLightsParent();
                }
                return lightsParent;
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

        protected override LightsType LightsType => LightsActive ? LightsType.External : LightsType.None;

        protected override bool LightsActive => LightsParent != null && LightsParent.activeInHierarchy;

        protected override float Capacity => PowerRelay != null ? PowerRelay.GetMaxPower() : 0f;

        protected override float Charge => PowerRelay != null ? PowerRelay.GetPower() : 0f;
    }
}
#endif
