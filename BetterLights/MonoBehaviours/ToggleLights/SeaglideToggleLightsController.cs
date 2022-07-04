using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class SeaglideToggleLightsController : AbstractToggleLightsController<Seaglide>
    {
        public override bool KeyDown => Input.GetKeyDown(Core.SeaglideSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.SeaglideSettings.LightsConsumption;
    }
}
