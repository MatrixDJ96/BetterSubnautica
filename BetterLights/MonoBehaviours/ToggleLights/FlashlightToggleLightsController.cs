using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class FlashlightToggleLightsController : AbstractToggleLightsController<FlashLight>
    {
        protected override bool KeyDown => Input.GetKeyDown(Core.FlashlightSettings.LightsButtonToggle);

        protected override float EnergyConsumption => Core.FlashlightSettings.LightsConsumption;
    }
}
