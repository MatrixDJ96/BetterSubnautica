using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class SubRootDebuggerController : AbstractDebuggerController<SubRoot>
    {
        public override bool ShowDebugInfo => Core.Settings.SubRootInfo;

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

        protected override LightsType LightsType
        {
            get
            {
                var lightsType = LightsType.None;
                return lightsType;
            }
        }

        protected override bool LightsActive
        {
            get
            {
                var lightsActive = false;
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
