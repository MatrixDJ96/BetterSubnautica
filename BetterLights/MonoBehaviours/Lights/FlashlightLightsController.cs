namespace BetterLights.MonoBehaviours.Lights
{
    public class FlashlightLightsController : AbstractLightsController<FlashLight>
    {
        protected override void UpdateSettings()
        {
            IntensityOffset = Core.FlashlightSettings.LightsIntensityOffset;
            RangeOffset = Core.FlashlightSettings.LightsRangeOffset;
        }
    }
}
