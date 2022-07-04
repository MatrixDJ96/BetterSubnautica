#if SUBNAUTICA
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class SeamothToggleLightsController : AbstractToggleLightsController<SeaMoth>
    {
        public override bool KeyDown => Input.GetKeyDown(Core.SeamothSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.SeamothSettings.LightsConsumption;

        public override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && component.GetPilotingMode();
        }
    }
}
#endif
