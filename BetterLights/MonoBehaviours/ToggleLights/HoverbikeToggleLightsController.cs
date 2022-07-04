#if BELOWZERO
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class HoverbikeToggleLightsController : AbstractToggleLightsController<Hoverbike>
    {
        public override bool KeyDown => Input.GetKeyDown(Core.HoverbikeSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.HoverbikeSettings.LightsConsumption;

        public override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && component.isPiloting;
        }
    }
}
#endif
