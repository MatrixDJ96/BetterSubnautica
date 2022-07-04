using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class FlashlightToggleLightsController : AbstractToggleLightsController<FlashLight>
    {
        public override bool KeyDown => Input.GetKeyDown(Core.FlashlightSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.FlashlightSettings.LightsConsumption;
    }
}
