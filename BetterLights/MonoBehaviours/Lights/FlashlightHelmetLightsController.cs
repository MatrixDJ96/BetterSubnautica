#if BELOWZERO
namespace BetterLights.MonoBehaviours.Lights
{
    public class FlashlightHelmetLightsController : AbstractLightsController<FlashlightHelmet>
    {
        protected override void UpdateSettings()
        {
            IntensityOffset = Core.FlashlightHelmetSettings.LightsIntensityOffset;
            RangeOffset = Core.FlashlightHelmetSettings.LightsRangeOffset;
        }
    }
}
#endif
