using BetterSubnautica.Enums;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class SubRootDebuggerController : AbstractDebuggerController<SubRoot>
    {
        public override bool ShowDebugInfo => Core.Settings.SubRootInfo;

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
                if (Component != null)
                {
                    capacity = Component.powerRelay.GetMaxPower();
                }
                return capacity;
            }
        }

        protected override float Charge
        {
            get
            {
                var charge = 0f;
                if (Component != null)
                {
                    charge = Component.powerRelay.GetPower();
                }
                return charge;
            }
        }
    }
}
