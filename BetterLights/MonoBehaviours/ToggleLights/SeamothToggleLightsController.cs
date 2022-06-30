#if SUBNAUTICA
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class SeamothToggleLightsController : AbstractToggleLightsController<SeaMoth>
    {
        private EnergyInterface energyInterface = null;
        private global::ToggleLights toggleLights = null;

        protected override bool KeyDown => Input.GetKeyDown(Core.SeamothSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.SeamothSettings.LightsConsumption;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                energyInterface = component.GetEnergyInterface();
                toggleLights = component.toggleLights;

                if (energyInterface == null || toggleLights == null)
                {
                    Destroy(this);
                    return;
                }

                lightsOnSound = toggleLights.lightsOnSound;
                onSound = toggleLights.onSound;
                lightsOffSound = toggleLights.lightsOffSound;
                offSound = toggleLights.offSound;

                toggleLights.energyPerSecond = 0f;
            }
        }

        protected override void Update()
        {
            base.Update();

            component.toggleLights.lightsActive = lightsActive;
        }

        protected override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && component.GetPilotingMode();
        }

        protected override bool IsPowered()
        {
            if (energyInterface != null)
            {
                return energyInterface.hasCharge;
            }

            return false;
        }

        protected override void ConsumeEnergy(float amount)
        {
            if (energyInterface != null)
            {
                energyInterface.ConsumeEnergy(amount);
            }
        }
    }
}
#endif
