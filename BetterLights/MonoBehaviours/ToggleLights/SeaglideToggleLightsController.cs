using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class SeaglideToggleLightsController : AbstractToggleLightsController<Seaglide>
    {
        protected override bool KeyDown => Input.GetKeyDown(Core.SeaglideSettings.LightsButtonToggle);

        protected override float EnergyConsumption => Core.SeaglideSettings.LightsConsumption;
    }
}
