#if BELOWZERO
namespace BetterLights.MonoBehaviours.Lights
{
    public class HoverbikeLightsController : AbstractLightsController<Hoverbike>
    {
        protected override void UpdateSettings()
        {
            IntensityOffset = Core.HoverbikeSettings.LightsIntensityOffset;
            RangeOffset = Core.HoverbikeSettings.LightsRangeOffset;
        }
    }
}
#endif
