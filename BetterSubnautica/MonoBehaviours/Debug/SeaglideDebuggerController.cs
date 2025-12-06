using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class SeaglideDebuggerController : AbstractDebuggerController<Seaglide>
    {
        public override bool ShowDebugInfo => Core.Settings.SeaglideInfo;

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

        private EnergyMixin energyMixin;
        private EnergyMixin EnergyMixin
        {
            get
            {
                if (energyMixin == null && Component != null)
                {
                    energyMixin = Component.GetEnergyMixin();
                }
                return energyMixin;
            }
        }

        protected override LightsType LightsType => LightsActive ? LightsType.External : LightsType.None;
        
        protected override bool LightsActive => LightsParent != null && LightsParent.activeInHierarchy;

        protected override float Capacity => EnergyMixin != null ? EnergyMixin.capacity : 0;

        protected override float Charge => EnergyMixin != null ? EnergyMixin.charge : 0;
    }
}
