#if BELOWZERO
using BetterLights.MonoBehaviours.ToggleLights;
using BetterSubnautica.MonoBehaviours;

namespace BetterLights.MonoBehaviours
{
    public class SeatruckLightsContainer : AbstractSingletonContainer<SeatruckLightsContainer, int, IToggleLightsController>
    {
        private SeatruckLightsContainer() { }
    }
}
#endif
