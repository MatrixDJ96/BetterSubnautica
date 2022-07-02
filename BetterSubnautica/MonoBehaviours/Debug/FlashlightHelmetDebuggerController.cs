#if BELOWZERO
using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class FlashlightHelmetDebuggerController : AbstractDebuggerController<FlashlightHelmet>
    {
        public override bool ShowDebugInfo => Core.Settings.FlashlightInfo;

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
                if (EnergyMixin != null)
                {
                    capacity += EnergyMixin.capacity;
                }
                return capacity;
            }
        }

        protected override float Charge
        {
            get
            {
                var charge = 0f;
                if (EnergyMixin != null)
                {
                    charge += EnergyMixin.charge;
                }
                return charge;
            }
        }
    }
}
#endif
