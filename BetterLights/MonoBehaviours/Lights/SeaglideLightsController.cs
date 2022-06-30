namespace BetterLights.MonoBehaviours.Lights
{
    public class SeaglideLightsController : AbstractLightsController<Seaglide>
    {
        protected override void UpdateSettings()
        {
            IntensityOffset = Core.SeaglideSettings.LightsIntensityOffset;
            RangeOffset = Core.SeaglideSettings.LightsRangeOffset;
        }
    }
}
