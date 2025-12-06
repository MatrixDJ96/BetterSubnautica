using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class SubRootDebuggerController : AbstractDebuggerController<SubRoot>
    {
        public override bool ShowDebugInfo => Core.Settings.SubRootInfo;

        protected override bool ShowLights { get; } = false;
        
        protected override LightsType LightsType => LightsType.None;

        protected override bool LightsActive => false;

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

        protected override float Capacity => PowerRelay != null ? PowerRelay.GetMaxPower() : 0f;

        protected override float Charge => PowerRelay != null ? PowerRelay.GetPower() : 0f;
    }
}
