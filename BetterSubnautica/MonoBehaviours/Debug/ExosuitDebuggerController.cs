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
                if (EnergyInterface != null)
                {
                    for (int i = 0; i < EnergyInterface.sources.Length; i++)
                    {
                        if (EnergyInterface.sources[i] != null)
                        {
                            capacity += EnergyInterface.sources[i].capacity;
                        }
                    }
                }
                return capacity;
            }
        }

        protected override float Charge
        {
            get
            {
                var charge = 0f;
                if (EnergyInterface != null)
                {
                    for (int i = 0; i < EnergyInterface.sources.Length; i++)
                    {
                        if (EnergyInterface.sources[i] != null)
                        {
                            charge += EnergyInterface.sources[i].charge;
                        }
                    }
                }
                return charge;
            }
        }
    }
}
