using System.Linq;
using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class ExosuitDebuggerController : AbstractDebuggerController<Exosuit>
    {
        public override bool ShowDebugInfo => Core.Settings.ExosuitInfo;

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

        private EnergyInterface energyInterface;
        private EnergyInterface EnergyInterface
        {
            get
            {
                if (energyInterface == null && Component != null)
                {
                    energyInterface = Component.GetEnergyInterface();
                }
                return energyInterface;
            }
        }

        protected override LightsType LightsType => LightsActive ? LightsType.External : LightsType.None;

        protected override bool LightsActive => LightsParent != null && LightsParent.activeInHierarchy;

        protected override float Capacity => EnergyInterface == null ? 0 : EnergyInterface.sources.Where(t => t != null).Sum(t => t.capacity);

        protected override float Charge => EnergyInterface == null ? 0 : EnergyInterface.sources.Where(t => t != null).Sum(t => t.charge);
    }
}
