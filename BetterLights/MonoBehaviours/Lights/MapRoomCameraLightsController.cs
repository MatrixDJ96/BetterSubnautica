namespace BetterLights.MonoBehaviours.Lights
{
    public class MapRoomCameraLightsController : AbstractLightsController<MapRoomCamera>
    {
        protected override void UpdateSettings()
        {
            IntensityOffset = Core.MapRoomCameraSettings.LightsIntensityOffset;
            RangeOffset = Core.MapRoomCameraSettings.LightsRangeOffset;
        }
    }
}
