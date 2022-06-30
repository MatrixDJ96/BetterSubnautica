using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class FlashlightToggleLightsController : AbstractToggleLightsController<FlashLight>
    {
        private EnergyMixin energyMixin = null;
        private global::ToggleLights toggleLights = null;

        protected override bool KeyDown => Input.GetKeyDown(Core.FlashlightSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.FlashlightSettings.LightsConsumption;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                energyMixin = component.GetEnergyMixin();
                toggleLights = component.toggleLights;

                if (energyMixin == null || toggleLights == null)
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

            toggleLights.lightsActive = lightsActive;
        }

        protected override bool IsPowered()
        {
            if (energyMixin != null)
            {
                return energyMixin.charge > 0f;
            }

            return false;
        }

        protected override void ConsumeEnergy(float amount)
        {
            if (energyMixin != null)
            {
                energyMixin.ConsumeEnergy(amount);
            }
        }
    }
}
