namespace BetterLights.MonoBehaviours.Lights
{
    public class ExosuitLightsController : AbstractLightsController<Exosuit>
    {
        protected override void UpdateSettings()
        {
            IntensityOffset = Core.ExosuitSettings.LightsIntensityOffset;
            RangeOffset = Core.ExosuitSettings.LightsRangeOffset;
        }
    }
}
