#if BELOWZERO
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class SeatruckToggleLightsController : AbstractToggleLightsController<SeaTruckSegment>
    {
        private SeaTruckLights seaTruckLights = null;
        private SeaTruckMotor seaTruckMotor = null;
        private PowerRelay powerRelay = null;

        protected override bool KeyDown => Input.GetKeyDown(Core.SeatruckSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.SeatruckSettings.LightsConsumption;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                seaTruckLights = component.gameObject.GetComponent<SeaTruckLights>();
                seaTruckMotor = component.gameObject.GetComponent<SeaTruckMotor>();
                powerRelay = component.GetPowerRelay();

                if (!component.IsMainSegment() || seaTruckLights == null || seaTruckLights.floodLight == null || seaTruckMotor == null)
                {
                    Destroy(this);
                    return;
                }

                lightsParent = seaTruckLights.floodLight;

                onSound = seaTruckLights.onSound;
                offSound = seaTruckLights.offSound;
            }
        }
        protected override void Update()
        {
            base.Update();

            seaTruckLights.lightsActive = lightsActive;
            seaTruckLights.SetSuppressionState(!lightsActive);
        }

        protected override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && seaTruckMotor.IsPiloted();
        }

        protected override bool IsPowered()
        {
            if (powerRelay != null)
            {
                return powerRelay.GetPower() > 0f;
            }

            return false;
        }

        protected override void ConsumeEnergy(float amount)
        {
            if (powerRelay != null)
            {
                powerRelay.ConsumeEnergy(amount, out var _);
            }
        }
    }
}
#endif
