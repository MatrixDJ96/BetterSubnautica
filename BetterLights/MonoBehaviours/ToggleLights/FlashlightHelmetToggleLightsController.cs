#if BELOWZERO
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class FlashlightHelmetToggleLightsController : AbstractToggleLightsController<FlashlightHelmet>
    {
        public override bool KeyDown => Input.GetKeyDown(Core.FlashlightHelmetSettings.LightsButtonToggle);

        public override float EnergyConsumption => 0f; // Core.FlashlightHelmetSettings.LightsConsumption

        public override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && !Player.main.isPiloting && Inventory.main.GetHeldObject() == null;
        }
    }
}
#endif
