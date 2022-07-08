#if BELOWZERO
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class SeatruckToggleLightsController : AbstractToggleLightsController<SeaTruckSegment>
    {
        protected override bool MandatoryToggleLights { get; } = false;
        protected override bool MandatoryLightsParent { get; } = false;

        private SeaTruckLights seaTruckLights = null;
        private SeaTruckMotor seaTruckMotor = null;

        protected override bool KeyDown => Input.GetKeyDown(Core.SeatruckSettings.LightsButtonToggle);

        protected override float EnergyConsumption => Core.SeatruckSettings.LightsConsumption;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                seaTruckLights = component.gameObject.GetComponent<SeaTruckLights>();
                seaTruckMotor = component.gameObject.GetComponent<SeaTruckMotor>();

                if (!component.IsMainSegment() || seaTruckLights == null || seaTruckLights.floodLight == null || seaTruckMotor == null)
                {
                    Destroy(this);
                    return;
                }

                lightsParent = seaTruckLights.floodLight;

                onSound = seaTruckLights.onSound;
                offSound = seaTruckLights.offSound;

                SeatruckLightsContainer.Instance.Dict[seaTruckLights.GetInstanceID()] = this;
            }
        }

        protected override void Update()
        {
            base.Update();

            seaTruckLights.lightsActive = lightsActive;
            seaTruckLights.SetSuppressionState(!lightsActive);
        }

        public override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && seaTruckMotor.IsPiloted();
        }
    }
}
#endif
